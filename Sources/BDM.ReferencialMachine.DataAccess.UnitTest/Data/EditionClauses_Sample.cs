using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class EditionClauses_Sample
    {
        public static ICollection<EditionClause> GetEditionClauses()
        {
            var lstEditionsClauses = new List<EditionClause>
            {
                new EditionClause{ Code = "CODE1", Label = "LabelClause1", Description = "DescriptionClause1", Type = "VOL"},
                new EditionClause{ Code = "CODE2", Label = "LabelClause2", Description = "DescriptionClause2", Type = null},
                new EditionClause{ Code = "CODE3", Label = "LabelClause3", Description = "DescriptionClause3", Type = "VOL"},
                new EditionClause{ Code = "CODE4", Label = "LabelClause4", Description = "DescriptionClause4", Type = null},
                new EditionClause{ Code = "CODE5", Label = "LabelClause5", Description = "DescriptionClause5", Type = "VOL"}
            };
            return lstEditionsClauses;
        }
    }
}
