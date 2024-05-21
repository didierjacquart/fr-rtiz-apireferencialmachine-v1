using System;

namespace BDM.ReferencialMachine.DataAccess.Models
{
    public class T_EDITION_CLAUSE
    {
        public Guid CLAUSE_ID { get; set; }
        public string CODE { get; set; }
        public string LABEL { get; set; }
        public string TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string CLAUSE_TYPE { get; set; }
    }
}
