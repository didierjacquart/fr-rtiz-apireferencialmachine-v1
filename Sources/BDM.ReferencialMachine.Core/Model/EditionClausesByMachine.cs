using System.Collections.Generic;

namespace BDM.ReferencialMachine.Core.Model
{
    public class EditionClausesByMachine
    {
        public string MachineCode { get; set; }

        public ICollection<EditionClause> EditionClauseList { get; set; }
    }
}
