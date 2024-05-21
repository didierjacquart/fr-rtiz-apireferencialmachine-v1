using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class PricingRates_Sample
    {
        public static ICollection<PricingRate> GetPricingRates()
        {
            var pricingRates = new List<PricingRate>
            {
                new PricingRate {Code = "AUTRES_DOMMAGES_EXTERNES", Rate = 1.1m},
                new PricingRate {Code = "INCENDIE_EXPLOSION_D_ENVIRONNEMENT", Rate = 1.2m},
                new PricingRate {Code = "GARANTIES_EN_TOUS_LIEUX", Rate = 1.3m},
                new PricingRate {Code = "BRIS_ET_DOMMAGES_INTERNES", Rate = 1.4m}
            };
            return pricingRates;
        }
    }
}
