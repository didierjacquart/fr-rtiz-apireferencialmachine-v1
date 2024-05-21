using System;
using System.Linq;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class T_MACHINE_SPECIFICATION_Sample
    {
        public static T_MACHINE_SPECIFICATION GetDatabaseMachineSpecification()
        {
            var tMachineSpecification = new T_MACHINE_SPECIFICATION
            {
                LABEL = "Label",
                NAME = "Name",
                CODE = "Code",
                DESCRIPTION = "Description",
                KEYWORDS = "Keywords1|Keywords2",
                IS_DELEGATED = true,
                IS_UNREFERENCED = false,
                IS_EXCLUDED = false,
                IS_OUT_OF_MACHINE_INSURANCE = false,
                AGE_LIMIT_ALLOWED = 12,
                ALL_PLACE_CORVERED = "FORBIDDEN",
                EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE = 12,
                MACHINE_RATE = 2,
                PRODUCT = "INVENTAIRE",
                SCORE = "1",
                START_DATETIME_SUBSCRIPTION_PERIOD = DateTime.UtcNow.Date,
                END_DATETIME_SUBSCRIPTION_PERIOD = null,
                T_PRICING_RATE = T_PRICING_RATE_Sample.GetDatabasePricingRates().ToList(),
                T_FAMILY = T_FAMILY_Sample.GetDatabaseFamilies().ToList(),
                EDITION_CLAUSE_CODES = "CODE1|CODE2|CODE3|CODE4|CODE5"
            };
            return tMachineSpecification;
        }
    }
}
