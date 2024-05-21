using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Interfaces
{
    public interface IMapperEditionClauses
    {
        ICollection<EditionClause> Map(IEnumerable<T_EDITION_CLAUSE> dbEditionClauses);
        ICollection<T_EDITION_CLAUSE> Map(IEnumerable<EditionClause> editionClauses);
        T_EDITION_CLAUSE Map(EditionClause editionClause);
    }
}
