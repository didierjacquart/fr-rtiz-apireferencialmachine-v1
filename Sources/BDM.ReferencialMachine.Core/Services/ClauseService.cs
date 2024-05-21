using System.Collections.Generic;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Services
{
    public class ClauseService : IClauseService
    {
        private readonly IClauseRepository _clauseRepository;
        
        public ClauseService(IClauseRepository clauseRepository)
        {
            _clauseRepository = clauseRepository;
        }
        
        public async Task<ICollection<EditionClause>> ReadClausesAsync()
        { 
            return await _clauseRepository.ReadClausesAsync();
        }

        public async Task CreateClauseAsync(EditionClause editionClause)
        {
            await _clauseRepository.CreateClauseAsync(editionClause);
        }

        public async Task UpdateClauseAsync(string clauseCode, EditionClause editionClause)
        {
            if (clauseCode != editionClause?.Code)
            {
                throw new BadRequestException(
                    ExceptionConstants.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_CODE,
                    ExceptionConstants.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_MESSAGE);
            }

            await _clauseRepository.UpdateClauseAsync(clauseCode, editionClause);
        }

        public async Task DeleteClauseAsync(string clauseCode)
        {
            await _clauseRepository.DeleteClauseAsync(clauseCode);
        }


    }
}
