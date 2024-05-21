using System.Collections.Generic;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class T_FAMILY_Sample
    {
        public static ICollection<T_FAMILY> GetDatabaseFamilies()
        {
            var families = new List<T_FAMILY>
            {
                new T_FAMILY {CODE = "Code_A", NAME = "Name_A", SUB_CODE = "Sub_Code_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B1", SUB_NAME = "Sub_Name_B1"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B2", SUB_NAME = "Sub_Name_B2"},
                new T_FAMILY {CODE = "Code_C", NAME = "Name_C", SUB_CODE = "ROOT", SUB_NAME = null}
            };
            return families;
        }
    }
}
