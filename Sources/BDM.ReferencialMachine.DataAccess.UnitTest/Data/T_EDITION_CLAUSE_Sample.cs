using System.Collections.Generic;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class T_EDITION_CLAUSE_Sample
    {
        public static ICollection<T_EDITION_CLAUSE> GetDatabaseEditionClauses()
        {
            var families = new List<T_EDITION_CLAUSE>
            {
                new T_EDITION_CLAUSE { CODE = "CODE1",LABEL = "LabelClause1", DESCRIPTION = "DescriptionClause1", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE2",LABEL = "LabelClause2", DESCRIPTION = "DescriptionClause2", TYPE = null },
                new T_EDITION_CLAUSE { CODE = "CODE3",LABEL = "LabelClause3", DESCRIPTION = "DescriptionClause3", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE4",LABEL = "LabelClause4", DESCRIPTION = "DescriptionClause4", TYPE = null },
                new T_EDITION_CLAUSE { CODE = "CODE5",LABEL = "LabelClause5", DESCRIPTION = "DescriptionClause5", TYPE = "VOL" }

            };
            return families;
        }
    }
}
