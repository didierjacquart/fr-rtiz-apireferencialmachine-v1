using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Interfaces
{
    public interface IMapperPricingRate
    {
        ICollection<PricingRate> Map(IEnumerable<T_PRICING_RATE> tPricingRates);
        ICollection<T_PRICING_RATE> Map(IEnumerable<PricingRate> pricingRates);
    }
}
