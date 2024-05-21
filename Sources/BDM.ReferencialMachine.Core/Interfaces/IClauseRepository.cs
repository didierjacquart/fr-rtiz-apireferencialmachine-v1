using System.Collections.Generic;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Interfaces
{
    public interface IClauseRepository
    {
        Task<ICollection<EditionClause>> ReadClausesAsync();
        Task CreateClauseAsync(EditionClause editionClause);
        Task UpdateClauseAsync(string clauseCode, EditionClause editionClause);
        Task DeleteClauseAsync(string clauseCode);
    }
}
