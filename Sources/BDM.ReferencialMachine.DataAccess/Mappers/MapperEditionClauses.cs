using System.Collections.Generic;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Mappers
{
    public class MapperEditionClauses : IMapperEditionClauses
    {
        public ICollection<EditionClause> Map(IEnumerable<T_EDITION_CLAUSE> dbEditionClauses)
        {
            return dbEditionClauses?.Where(dbEditionClause => dbEditionClause != null).Select(dbEditionClause => Map(dbEditionClause!)).ToList();
        }

        private static EditionClause Map(T_EDITION_CLAUSE dbEditionClause)
        {
            var editionClause = new EditionClause
            {
                Code = dbEditionClause.CODE,
                Description = dbEditionClause.DESCRIPTION,
                Label = dbEditionClause.LABEL,
                Type = dbEditionClause.TYPE,
                ClauseType = dbEditionClause.CLAUSE_TYPE
            };
            
            return editionClause;
        }

        public T_EDITION_CLAUSE Map(EditionClause editionClause)
        {
            var dbEditionClause = new T_EDITION_CLAUSE
            {
                CODE = editionClause!.Code,
                LABEL = editionClause!.Label,
                DESCRIPTION = editionClause!.Description,
                TYPE = editionClause!.Type,
                CLAUSE_TYPE = editionClause!.ClauseType
            };
            return dbEditionClause;
        }

        public ICollection<T_EDITION_CLAUSE> Map(IEnumerable<EditionClause> editionClauses)
        {
            var dbEditionsClauses = new List<T_EDITION_CLAUSE>();
            if (editionClauses == null)
            {
                return dbEditionsClauses;
            }

            dbEditionsClauses.AddRange(
                editionClauses
                    .Select(editionClause => 
                        new T_EDITION_CLAUSE
                        {
                            CODE = editionClause.Code, 
                            LABEL = editionClause.Label, 
                            TYPE = editionClause.Type, 
                            DESCRIPTION = editionClause.Description,
                            CLAUSE_TYPE = editionClause.ClauseType
                        }));

            return dbEditionsClauses;
        }
    }
}
