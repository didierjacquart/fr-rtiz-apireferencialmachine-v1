using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AF.WSIARD.Exception.Core;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Mappers
{
    public class MapperPricingRateTests
    {
        [Fact]
        public void Should_Map_PricingRateToBusiness_Nominal_Call()
        {
            var expectedPricingRates = new List<T_PRICING_RATE>
            {
                new T_PRICING_RATE {CODE = "ADE", RATE = 1.1m},
                new T_PRICING_RATE {CODE = "INC", RATE = 1.2m},
                new T_PRICING_RATE {CODE = "GTL", RATE = 1.3m},
                new T_PRICING_RATE {CODE = "BDI", RATE = 1.4m},
                new T_PRICING_RATE {CODE = "VOL", RATE = 1.5m}
            };

            var mapper = new MapperPricingRate();

            var pricingRatesResult = mapper.Map(expectedPricingRates);

            Assert.NotNull(pricingRatesResult);
            Assert.Equal(expectedPricingRates.Count, pricingRatesResult.Count);

            foreach (var expectedPricingRate in expectedPricingRates)
            {
                expectedPricingRate.CODE = MapperPricingRate.ConvertPrincingCodeToBusiness(expectedPricingRate.CODE);
            }
            
            foreach (var pricingRate in pricingRatesResult)
            {
                Assert.True(expectedPricingRates.Select(x => x.CODE).Contains(pricingRate.Code));
                Assert.True(expectedPricingRates.Select(x => x.RATE).Contains(pricingRate.Rate));
            }
        }

        [Fact]
        public void Should_Not_Map_DatabasePricingRate_When_Input_Is_Null()
        {
           var mapper = new MapperPricingRate();

            var pricingRatesResult = mapper.Map((Collection<PricingRate>)null);

            Assert.NotNull(pricingRatesResult);
            Assert.Empty(pricingRatesResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("TOTO")]
        public void Should_Map_PricingRateToBusiness_Throw_When_Code_Not_Supported(string notSupportedCodeValue)
        {
            var  exception = Assert.Throws<FunctionalException>(() => MapperPricingRate.ConvertPrincingCodeToBusiness(notSupportedCodeValue));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_MESSAGE(notSupportedCodeValue), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("TOTO")]
        public void Should_Map_PricingRateToDatabase_Throw_When_Code_Not_Supported(string notSupportedCodeValue)
        {
            var exception = Assert.Throws<FunctionalException>(() => MapperPricingRate.ConvertPrincingCodeToDataBase(notSupportedCodeValue));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.NOT_AUTHORIZED_PRICING_CODE_VALUE_MESSAGE(notSupportedCodeValue), exception.Message);
        }

        [Fact]
        public void Should_Map_PricingRateToDatabase_Nominal_Call()
        {
            var expectedPricingRates = new List<PricingRate>
            {
                new PricingRate {Code = "INCENDIE_EXPLOSION_D_ENVIRONNEMENT", Rate = 1.1m},
                new PricingRate {Code = "BRIS_ET_DOMMAGES_INTERNES", Rate = 1.2m},
                new PricingRate {Code = "AUTRES_DOMMAGES_EXTERNES", Rate = 1.3m},
                new PricingRate {Code = "GARANTIES_EN_TOUS_LIEUX", Rate = 1.4m},
                new PricingRate {Code = "VOL", Rate = 1.5m}
            };

            var mapper = new MapperPricingRate();

            var pricingRatesResult = mapper.Map(expectedPricingRates);

            Assert.NotNull(pricingRatesResult);
            Assert.Equal(expectedPricingRates.Count, pricingRatesResult.Count);

            foreach (var expectedPricingRate in expectedPricingRates)
            {
                expectedPricingRate.Code = MapperPricingRate.ConvertPrincingCodeToDataBase(expectedPricingRate.Code);
            }

            foreach (var pricingRate in pricingRatesResult)
            {
                Assert.True(expectedPricingRates.Select(x => x.Code).Contains(pricingRate.CODE));
                Assert.True(expectedPricingRates.Select(x => x.Rate).Contains(pricingRate.RATE));
            }
        }

        [Fact]
        public void Should_Not_Map_BusinessPricingRate_When_Input_Is_Null()
        {
            var mapper = new MapperPricingRate();

            var pricingRatesResult = mapper.Map((Collection<T_PRICING_RATE>)null);

            Assert.NotNull(pricingRatesResult);
            Assert.Empty(pricingRatesResult);
        }

    }
}
