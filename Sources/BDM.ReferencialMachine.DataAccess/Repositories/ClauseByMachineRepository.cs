using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ClauseByMachineRepository : IClauseByMachineRepository
    {
        private readonly MachineContext _context;
        private readonly ILogger<ClauseByMachineRepository> _logger;
        private readonly IMapperEditionClauses _mapper;
        
        public ClauseByMachineRepository(MachineContext context, IMapperEditionClauses mapper, ILogger<ClauseByMachineRepository> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ICollection<EditionClausesByMachine>> ReadMachineClausesListAsync(IEnumerable<string> machineCodes)
        {
            var query = _context.GetQueryditionClauseLists(machineCodes.ToArray());

            ClauseCodesByMachine[] clausesCodesByMachines = null;
            await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
            {
                clausesCodesByMachines = await query.ToArrayAsync();
            }).ConfigureAwait(false);
            
            var editionClausesByMachines = new List<EditionClausesByMachine>();
            foreach (var clauseCodesByMachine in clausesCodesByMachines)
            {
                ICollection<T_EDITION_CLAUSE> clauses = null;
                await RetryHelper.Retry(_logger).ExecuteAsync(async () =>
                {
                    clauses = await _context.T_EDITION_CLAUSE.Where(x => clauseCodesByMachine.EditionClauseCodes.Contains(x.CODE)).ToArrayAsync();
                }).ConfigureAwait(false);

                var editionClausesByMachine = new EditionClausesByMachine
                {
                    MachineCode = clauseCodesByMachine.MachineCode,
                    EditionClauseList = _mapper.Map(clauses)
                };
                editionClausesByMachines.Add(editionClausesByMachine);
            }


            return editionClausesByMachines;

        }
    }
}
