using System.Collections.Generic;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class T_PRICING_RATE_Sample
    {
        public static ICollection<T_PRICING_RATE> GetDatabasePricingRates()
        {
            var pricingRates = new List<T_PRICING_RATE>
            {
                new T_PRICING_RATE {CODE = "ADE", RATE = 1.1m},
                new T_PRICING_RATE {CODE = "INC", RATE = 1.2m},
                new T_PRICING_RATE {CODE = "GTL", RATE = 1.3m},
                new T_PRICING_RATE {CODE = "BDI", RATE = 1.4m}
            };
            return pricingRates;

        }
    }
}
