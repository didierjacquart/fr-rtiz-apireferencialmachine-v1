using System.Collections.Generic;

namespace BDM.ReferencialMachine.DataAccess.Models
{
    public class MachineAndRelatedObjects
    {
        public T_MACHINE_SPECIFICATION T_MACHINE_SPECIFICATION { get; set; }
        public IEnumerable<T_FAMILY> T_FAMILY { get; set; }
        public IEnumerable<T_EDITION_CLAUSE> T_EDITION_CLAUSE { get; set; }
    }
}
