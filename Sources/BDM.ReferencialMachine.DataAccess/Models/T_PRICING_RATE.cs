using System;

namespace BDM.ReferencialMachine.DataAccess.Models
{
    public class T_PRICING_RATE
    {
        public Guid PRICING_RATE_ID { get; set; }
        public string CODE { get; set; }
        public decimal? RATE { get; set; }
        public Guid? FK_MACHINE_ID { get; set; }

        public virtual T_MACHINE_SPECIFICATION T_MACHINE_SPECIFICATION { get; set; }
    }
}
