using System;
using System.Collections.Generic;

namespace BDM.ReferencialMachine.DataAccess.Models
{
    public class T_FAMILY
    {
        public T_FAMILY()
        {
            T_MACHINE_SPECIFICATION = new List<T_MACHINE_SPECIFICATION>();
        }

        public Guid FAMILY_ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string SUB_CODE { get; set; }
        public string SUB_NAME { get; set; }

        public virtual List<T_MACHINE_SPECIFICATION> T_MACHINE_SPECIFICATION { get; set; }
    }
}
