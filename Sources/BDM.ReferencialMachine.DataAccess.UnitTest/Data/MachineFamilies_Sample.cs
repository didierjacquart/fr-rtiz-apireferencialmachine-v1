using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class MachineFamilies_Sample
    {
        public static ICollection<Family> GetMachineFamilies()
        {
            var businessFamilies = new List<Family>
            {
                new Family
                {
                    Code = "Code_A",
                    Name = "Name_A",
                    SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Name = "Sub_Name_A", Code = "Sub_Code_A"}
                    }
                },
                new Family
                {
                    Code = "Code_B",
                    Name = "Name_B",
                    SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Name = "Sub_Name_B1", Code = "Sub_Code_B1"},
                        new SubFamily {Name = "Sub_Name_B2", Code = "Sub_Code_B2"}
                    }
                },
                new Family
                {
                    Code = "Code_C",
                    Name = "Name_C"
                }

            };

            return businessFamilies;
        }
    }
}
