using System;

namespace BDM.ReferencialMachine.DataAccess.Models
{
    public class T_RISK_PRECISION
    {
        public Guid RISK_PRECISION_ID { get; set; }
        public string CODE { get; set; }
        public string MACHINE_CODE { get; set; }
        public string? LABEL { get; set; }
        public string? DETAIL { get; set; }
    }
}
