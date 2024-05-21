using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Helper;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BDM.ReferencialMachine.DataAccess.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly MachineContext _context;
        private readonly ILogger<IFamilyRepository> _logger;
        private readonly IMapperFamilies _mapperFamilies;

        private const int DUPLICATE_KEY_EXCEPTION_NUMBER = 2601;

        public FamilyRepository(MachineContext context, IMapperFamilies mapperFamilies, ILogger<IFamilyRepository> logger)
        {
            _context = context;
            _mapperFamilies = mapperFamilies;
            _logger = logger;
        }

        public async Task<ICollection<Family>> ReadFamiliesAsync()
        {
            T_FAMILY[] tFamilies = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                tFamilies = await _context.T_FAMILY.AsNoTracking().ToArrayAsync();
            }).ConfigureAwait(false);

            return _mapperFamilies.Map(tFamilies);
        }

       public async Task CreateFamilyAsync(Family family)
        {
            var tFamilies = _mapperFamilies.Map(family);
            try
            {
                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    await _context.T_FAMILY.AddRangeAsync(tFamilies);
                    await _context.SaveChangesAsync();
                });
            }
            catch (DbUpdateException exception)
            {
                if ((exception.InnerException as SqlException)?.Number == DUPLICATE_KEY_EXCEPTION_NUMBER)
                {
                    throw new BadRequestException(ExceptionConstants.FAMILY_ALREADY_EXIST_EXCCEPTION_CODE,
                        ExceptionConstants.FAMILY_ALREADY_EXIST_EXCCEPTION_MESSAGE(family?.Code));
                }
                throw;
            }
        }

        public async Task DeleteFamilyAsync(string familyCode)
        {
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                var databaseSubFamilies = await GetDatabaseFamiliesAsync(familyCode);

                if (!databaseSubFamilies.Any())
                {
                    throw new NotFoundException(ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_CODE,
                        ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_MESSAGE(databaseSubFamilies.First().CODE));
                }

                if (!databaseSubFamilies.All(x => x.T_MACHINE_SPECIFICATION == null || !x.T_MACHINE_SPECIFICATION.Any()))
                {
                    throw new BadRequestException(ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_CODE,
                        ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_MESSAGE);
                }
                _context.T_FAMILY.RemoveRange(databaseSubFamilies);
                await _context.SaveChangesAsync();
            });
        }

        public async Task UpdateFamilyAsync(string familyCode, Family family)
        {
            var targetFamilies = _mapperFamilies.Map(family).ToArray();                
            var databaseFamilies = await GetDatabaseFamiliesAsync(familyCode);

            ReAttachMachineToTargetFamilies(databaseFamilies, targetFamilies);

            CheckMachineAttachedToFamiliesToBeDeleted(databaseFamilies, targetFamilies);

            _context.T_FAMILY.RemoveRange(databaseFamilies.Where(x => x.SUB_CODE != null));
            _context.T_FAMILY.AddRange(targetFamilies.Where(x => x.SUB_CODE != null));

            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                await _context.SaveChangesAsync();
            });
        }

        private static void ReAttachMachineToTargetFamilies(IEnumerable<T_FAMILY> databaseFamilies, IEnumerable<T_FAMILY> targetFamilies)
        {
            if (databaseFamilies == null || targetFamilies == null)
            {
                return;
            }
            var attachedMachines = databaseFamilies.Select(x => new { x.CODE, x.SUB_CODE, x.T_MACHINE_SPECIFICATION });

            foreach (var targetFamily in targetFamilies)
            {
                targetFamily.T_MACHINE_SPECIFICATION =
                    attachedMachines.FirstOrDefault(x => x.CODE == targetFamily.CODE && x.SUB_CODE == targetFamily.SUB_CODE)
                        ?.T_MACHINE_SPECIFICATION;
            }
        }

        private static void CheckMachineAttachedToFamiliesToBeDeleted(IEnumerable<T_FAMILY> databaseFamilies, IEnumerable<T_FAMILY> targetFamilies)
        {
            if (databaseFamilies == null || targetFamilies == null)
            {
                return;
            }
            foreach (var databaseFamily in databaseFamilies)
            {
                var isFamilyToDeted =
                    !targetFamilies.Any(x =>
                        x.CODE == databaseFamily.CODE && x.SUB_CODE == databaseFamily.SUB_CODE);
                if (isFamilyToDeted && databaseFamily.T_MACHINE_SPECIFICATION.Any())
                {
                    throw new BadRequestException(ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_CODE,
                        ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_MESSAGE);
                }
            }
        }

        private async Task<T_FAMILY[]> GetDatabaseFamiliesAsync(string familyCode)
        {
            T_FAMILY[] tFamilies = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                tFamilies = await _context.T_FAMILY.Include(x => x.T_MACHINE_SPECIFICATION)
                    .Where(x => x.CODE == familyCode).ToArrayAsync();

                if (!tFamilies.Any())
                {
                    throw new NotFoundException(ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_CODE,
                        ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_MESSAGE(familyCode));
                }
            });

            return tFamilies;
        }
    }
}
