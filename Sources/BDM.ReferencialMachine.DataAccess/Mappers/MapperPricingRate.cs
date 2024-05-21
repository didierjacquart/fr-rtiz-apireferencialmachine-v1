using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AF.WSIARD.Exception.Core;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;


[assembly: InternalsVisibleTo("BDM.ReferencialMachine.DataAccess.UnitTest")]
namespace BDM.ReferencialMachine.DataAccess.Mappers
{
    public class MapperPricingRate : IMapperPricingRate
    {
       
        private static readonly Dictionary<string, string> BusinessPricingCode = new Dictionary<string, string>
        {
            { "INC", "INCENDIE_EXPLOSION_D_ENVIRONNEMENT" },
            { "BDI", "BRIS_ET_DOMMAGES_INTERNES" },
            { "ADE", "AUTRES_DOMMAGES_EXTERNES" },
            { "GTL", "GARANTIES_EN_TOUS_LIEUX" },
            { "VOL", "VOL" }
        };

        public ICollection<PricingRate> Map(IEnumerable<T_PRICING_RATE> tPricingRates)
        {
            if (tPricingRates == null || !tPricingRates.Any())
            {
                return new List<PricingRate>();
            }

            var pricingRateArray = tPricingRates.Select(pricingRate => new PricingRate
            {
                Code =  ConvertPrincingCodeToBusiness(pricingRate.CODE), 
                Rate = pricingRate.RATE
            });

            return pricingRateArray.ToArray();
        }

        public ICollection<T_PRICING_RATE> Map(IEnumerable<PricingRate> pricingRates)
        {
            if (pricingRates == null || !pricingRates.Any())
            {
                return new List<T_PRICING_RATE>();
            }
            var pricingRateArray = pricingRates.Select(pricingRate => new T_PRICING_RATE
            {
                CODE = ConvertPrincingCodeToDataBase(pricingRate.Code),
                RATE = pricingRate.Rate
            });

            return pricingRateArray.ToArray();
        }

        public static string ConvertPrincingCodeToDataBase(string businessValue)
        {
            if (string.IsNullOrEmpty(businessValue) || !BusinessPricingCode.ContainsValue(businessValue))
            {
                throw new FunctionalException(ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_CODE, ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_MESSAGE(businessValue));
            }
            return BusinessPricingCode.FirstOrDefault(x => x.Value == businessValue).Key;
        }

        public static string ConvertPrincingCodeToBusiness(string code)
        {
            if (string.IsNullOrEmpty(code) || !BusinessPricingCode.ContainsKey(code))
            {
                throw new FunctionalException(ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_CODE, ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_MESSAGE(code));
            }
            return BusinessPricingCode[code];
        }   

    }
}
