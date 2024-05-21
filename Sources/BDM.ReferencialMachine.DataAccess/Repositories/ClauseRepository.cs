using System.Collections.Generic;
using System.Threading.Tasks;
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

namespace BDM.ReferencialMachine.DataAccess.Repositories
{
    public class ClauseRepository : IClauseRepository
    {
        private readonly MachineContext _context;
        private readonly ILogger<ClauseRepository> _logger;
        private readonly IMapperEditionClauses _mapper;

        public ClauseRepository(MachineContext context,
            IMapperEditionClauses mapper,
            ILogger<ClauseRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<EditionClause>> ReadClausesAsync()
        {
            ICollection<EditionClause> lstEditionClause = new List<EditionClause>();
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                lstEditionClause = _mapper.Map(await _context.T_EDITION_CLAUSE.ToArrayAsync());
            });
            return lstEditionClause;
        }

        public async Task CreateClauseAsync(EditionClause editionClause)
        {
            if (string.IsNullOrEmpty(editionClause?.Code) || string.IsNullOrEmpty(editionClause.Label))
            {
                throw new BadRequestException(ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE,
                    ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE);
            }

            var dbClause = await _context.T_EDITION_CLAUSE.FirstOrDefaultAsync(x => x.CODE == editionClause.Code);
            if (dbClause != null)
            {
                  throw new BadRequestException(ExceptionConstants.CLAUSE_ALREADY_EXIST_EXCCEPTION_CODE,
                        ExceptionConstants.CLAUSE_ALREADY_EXIST_EXCCEPTION_MESSAGE(editionClause.Code));
            }

            var tEditionClauses = _mapper.Map(editionClause);
            await _context.T_EDITION_CLAUSE.AddRangeAsync(tEditionClauses);
            
            await RetryHelper.Retry(_logger).ExecuteAsync(async () => 
            {
                await _context.SaveChangesAsync();
            });
            
        }

        public async Task UpdateClauseAsync(string clauseCode, EditionClause editionClause)
        {
            if (string.IsNullOrEmpty(editionClause?.Code) || string.IsNullOrEmpty(editionClause.Label))
            {
                throw new BadRequestException(ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE,
                    ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE);
            }

            var dbEditionClause = await GetDatabaseEditionClauseAsync(clauseCode);

            dbEditionClause.LABEL = editionClause.Label;
            dbEditionClause.TYPE = editionClause.Type;
            dbEditionClause.DESCRIPTION = editionClause.Description;

            _context.T_EDITION_CLAUSE.Update(dbEditionClause);

            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                await _context.SaveChangesAsync();
            });
        }

        public async Task DeleteClauseAsync(string clauseCode)
        {
            var dbEditionClause = await GetDatabaseEditionClauseAsync(clauseCode);

            T_MACHINE_SPECIFICATION machineWithClause = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                machineWithClause = await _context.T_MACHINE_SPECIFICATION.AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                        !string.IsNullOrEmpty(x.EDITION_CLAUSE_CODES) && x.EDITION_CLAUSE_CODES.Contains(clauseCode));
            }).ConfigureAwait(false);
            if (machineWithClause != null)
            {
                throw new BadRequestException(ExceptionConstants.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_CODE,
                    ExceptionConstants.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_MESSAGE(clauseCode));
            }
            
            _context.Remove(dbEditionClause);
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                await _context.SaveChangesAsync();
            });
        }

        private async Task<T_EDITION_CLAUSE> GetDatabaseEditionClauseAsync(string clauseCode)
        {
            T_EDITION_CLAUSE tEditionClause = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                tEditionClause = await _context.T_EDITION_CLAUSE.FirstOrDefaultAsync(x => x.CODE == clauseCode);

            });
            if (tEditionClause == null)
            {
                throw new NotFoundException(ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_CODE,
                    ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE(clauseCode));
            }
            return tEditionClause;
        }
    }
   
}
