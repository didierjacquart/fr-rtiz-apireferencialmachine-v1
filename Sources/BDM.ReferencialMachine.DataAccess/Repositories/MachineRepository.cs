using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Helper;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MC = BDM.ReferencialMachine.Core.Constants.MachineConstants;

[assembly: InternalsVisibleTo("BDM.ReferencialMachine.DataAccess.UnitTest")]
namespace BDM.ReferencialMachine.DataAccess.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IMapperDatabaseToBusiness _mapperDatabaseToBusiness;
        private readonly IMapperBusinessToDatabase _mapperBusinessToDatabase;
        private readonly MachineContext _context;
        private readonly ILogger<MachineRepository> _logger;

        public MachineRepository(MachineContext context,
            IMapperDatabaseToBusiness mapperDatabaseToBusiness,
            IMapperBusinessToDatabase mapperBusinessToDatabase,
            ILogger<MachineRepository> logger)
        {
            _mapperDatabaseToBusiness = mapperDatabaseToBusiness;
            _mapperBusinessToDatabase = mapperBusinessToDatabase;
            _logger = logger;
            _context = context;
        }

        public async Task<MachineSpecification> ReadMachineAsync(string machineCode)
        {
            var query = _context.GetQueryForMachineWithAllDetails(machineCode);

            T_MACHINE_SPECIFICATION dbMachineSpecification = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                dbMachineSpecification = await query.FirstOrDefaultAsync();
            }).ConfigureAwait(false);

            if (dbMachineSpecification == null)
            {
                throw new NotFoundException(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_MESSAGE(machineCode));
            }

            IEnumerable<T_EDITION_CLAUSE> dbClauses = null;
            if (!string.IsNullOrEmpty(dbMachineSpecification.EDITION_CLAUSE_CODES))
            {
                var codes = dbMachineSpecification.EDITION_CLAUSE_CODES?.Split("|").ToArray();
             
                await RetryHelper.Retry(_logger).ExecuteAndCaptureAsync(async () =>
                {
                    dbClauses = await _context.T_EDITION_CLAUSE.AsNoTracking().Where(x => codes.Contains(x.CODE))
                        .ToArrayAsync();
                }).ConfigureAwait(false);
            }

            IEnumerable<T_RISK_PRECISION> dbRiskPrecisions = null;
            if (dbMachineSpecification.PRODUCT == MC.PARK || dbMachineSpecification.PRODUCT == MC.RENTAL_MACHINE)
            {
                await RetryHelper.Retry(_logger).ExecuteAndCaptureAsync(async () =>
                {
                    dbRiskPrecisions = await _context.T_RISK_PRECISION.AsNoTracking().Where(x => x.MACHINE_CODE == machineCode).ToArrayAsync();
                }).ConfigureAwait(false);
            }

            return _mapperDatabaseToBusiness.Map(dbMachineSpecification, dbClauses, dbRiskPrecisions);

        }

        public async Task<int> CreateMachineAsync(MachineSpecification machineSpecification)
        {
            var relatedElementMachine = _mapperBusinessToDatabase.Map(machineSpecification);
            int result = 0;
            try
            {
                AddExistingFamiliesToMachine(relatedElementMachine.T_MACHINE_SPECIFICATION, relatedElementMachine.T_FAMILY);

                var dbClauses = relatedElementMachine.T_EDITION_CLAUSE;
                await CheckClausesExistsAsync(dbClauses);

                if (dbClauses != null && dbClauses.Any())
                {
                    relatedElementMachine.T_MACHINE_SPECIFICATION.EDITION_CLAUSE_CODES = string.Join("|", dbClauses.Select(x => x.CODE));
                }
                await _context.AddAsync(relatedElementMachine.T_MACHINE_SPECIFICATION);

                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    result = await _context.SaveChangesAsync();
                }).ConfigureAwait(false);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e.Message, e);
                throw new TechnicalException(ExceptionConstants.DB_UPDATE_EXCEPTION_MESSAGE, e.InnerException);
            }
            return result;
        }

        public async Task<Collection<MachineSpecification>> ReadAllMachinesAsync(string product)
        {
            var query = _context.GetQueryForAllMachines(product);

            T_MACHINE_SPECIFICATION[] tLstMachineSpecification = null;

            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                tLstMachineSpecification = await query.ToArrayAsync();
            });


            if (!tLstMachineSpecification.Any())
            {
                return new Collection<MachineSpecification>();
            }
            
            T_RISK_PRECISION[] tLstRiskPrecisions = null;
            if (product == MC.PARK || product == MC.RENTAL_MACHINE)
            {
                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    tLstRiskPrecisions = await _context.T_RISK_PRECISION.AsNoTracking().ToArrayAsync(); 
                });
            }

            var allMachines = _mapperDatabaseToBusiness.Map(tLstMachineSpecification, tLstRiskPrecisions);

            return allMachines;
        }

        public Task UpdateMachineAsync(string machineCode, MachineSpecification machineSpecification)
        {
            if (machineSpecification == null)
            {
                throw new ArgumentNullException(nameof(machineSpecification));
            }
            return PrivateUpdateMachineAsync(machineCode, machineSpecification);
        }

        public async Task<MachineSpecification[]> ReadMachineListAsync(string[] machineCodes)
        {
            var query = _context.GetQueryForMachineList(machineCodes);

            T_MACHINE_SPECIFICATION[] machineList = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                machineList = await query.ToArrayAsync();
            });

            var machineNotFoundCode = machineCodes?.Except(machineList.Select(machine => machine.CODE)).ToArray();
            if (machineNotFoundCode != null && machineNotFoundCode.Any())
            {
                throw new BadRequestException(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE,
                    ExceptionConstants.MACHINES_NOT_FOUND_EXCEPTION_MESSAGE(string.Join(";", machineNotFoundCode)));
            }

            T_RISK_PRECISION[] riskPrecisions = null;
            if (machineList.All(x => x.PRODUCT == MC.PARK || x.PRODUCT == MC.RENTAL_MACHINE))
            {
                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    riskPrecisions = await _context.T_RISK_PRECISION.AsNoTracking().Where(x => machineCodes.Contains(x.MACHINE_CODE)).ToArrayAsync();
                });
            }

            return _mapperDatabaseToBusiness.Map(machineList, riskPrecisions).ToArray();
        }

        private async Task PrivateUpdateMachineAsync(string machineCode, MachineSpecification machineSpecification)
        {
            try
            {
                var query = _context.GetQueryForUpdate(machineCode);

                T_MACHINE_SPECIFICATION databaseMachineSpecification = null;
                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    databaseMachineSpecification = await query.FirstOrDefaultAsync();
                });

                if (databaseMachineSpecification == null)
                {
                    throw new NotFoundException(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE,
                        ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_MESSAGE(machineCode));
                }

                if (string.IsNullOrEmpty(machineSpecification.Code))
                {
                    machineSpecification.Code = databaseMachineSpecification.CODE;
                }

                var relatedElementMachine = _mapperBusinessToDatabase.Map(machineSpecification);
                RemoveExistingMachine(databaseMachineSpecification);
                await AddNewMachineAsync(relatedElementMachine);
                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    await _context.SaveChangesAsync();
                }).ConfigureAwait(false);
            }
            catch (DbUpdateException e)
            {
                throw new TechnicalException(ExceptionConstants.DB_UPDATE_EXCEPTION_MESSAGE, e.InnerException);
            }
        }

        private async Task AddNewMachineAsync(MachineAndRelatedObjects machineAndRelatedObjects)
        {
            AddExistingFamiliesToMachine(machineAndRelatedObjects.T_MACHINE_SPECIFICATION, machineAndRelatedObjects.T_FAMILY);
            await CheckClausesExistsAsync(machineAndRelatedObjects.T_EDITION_CLAUSE);
            _context.Add(machineAndRelatedObjects.T_MACHINE_SPECIFICATION);
        }

        private void RemoveExistingMachine(T_MACHINE_SPECIFICATION databaseMachineSpecification)
        {
            _context.RemoveRange(databaseMachineSpecification.T_PRICING_RATE);
            _context.Remove(databaseMachineSpecification);
        }

        private void AddExistingFamiliesToMachine(T_MACHINE_SPECIFICATION tMachineSpecification, IEnumerable<T_FAMILY> dbFamilies)
        {
            if (dbFamilies == null)
            {
                return;
            }

            foreach (var dbFamily in dbFamilies)
            {
                var existingFamily = _context.T_FAMILY
                    .FirstOrDefault(c => c.CODE == dbFamily.CODE && c.SUB_CODE == dbFamily.SUB_CODE);

                if (existingFamily == null)
                {
                    throw new BadRequestException(ExceptionConstants.NO_MATCHING_FAMILY_EXCEPTION_CODE,
                        ExceptionConstants.NO_MATCHING_FAMILY_EXCEPTION_MESSAGE(dbFamily.CODE, dbFamily.SUB_CODE));
                }
                tMachineSpecification.T_FAMILY.Add(existingFamily);
            }
        }

        private async Task CheckClausesExistsAsync(IEnumerable<T_EDITION_CLAUSE> dbEditionClauses)
        {
            if (dbEditionClauses == null)
            {
                return;
            }

            var inputCodes = dbEditionClauses.Select(c => c.CODE).ToList();

            var foundCodes = new List<string>();
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                foundCodes = await _context.T_EDITION_CLAUSE.Where(c => inputCodes.Contains(c.CODE)).Select(c => c.CODE).ToListAsync();
            }).ConfigureAwait(false);

            var codesNotFound = inputCodes.Except(foundCodes).ToArray();

            if (codesNotFound.Any())
            {
                throw new BadRequestException(ExceptionConstants.NO_MATCHING_CLAUSE_EXCEPTION_CODE, ExceptionConstants.NO_MATCHING_CLAUSE_EXCEPTION_MESSAGE(codesNotFound.First()));
            }

        }
    }
}
