using System;
using System.Collections.Generic;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Mappers
{
    public class MapperBusinessToDatabaseTests
    {
        private readonly Mock<IMapperFamilies> _mapperFamilies;
        private readonly Mock<IMapperPricingRate> _mapperPricingRates;
        private readonly Mock<IMapperEditionClauses> _mapperEditionClause;
        private readonly MapperBusinessToDatabase _mapper;

        public MapperBusinessToDatabaseTests()
        {
            _mapperFamilies = new Mock<IMapperFamilies>();
            _mapperEditionClause = new Mock<IMapperEditionClauses>();
            _mapperPricingRates = new Mock<IMapperPricingRate>();
            _mapper = new MapperBusinessToDatabase(_mapperPricingRates.Object, _mapperFamilies.Object, _mapperEditionClause.Object);
        }


        [Fact]
        public void Should_Map_When_Nominal_Call()
        {
            _mapperFamilies.Setup(x => x.Map(It.IsAny<ICollection<Family>>()))
                .Returns(T_FAMILY_Sample.GetDatabaseFamilies());

            _mapperPricingRates.Setup(x => x.Map(It.IsAny<ICollection<PricingRate>>()))
                .Returns(T_PRICING_RATE_Sample.GetDatabasePricingRates());

            _mapperEditionClause.Setup(x => x.Map(It.IsAny<ICollection<EditionClause>>()))
                .Returns(T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses);
            
            var relatedElementMachine = _mapper.Map(MachineSpecification_Sample.GetMachineSpecification());

            var machine = relatedElementMachine.T_MACHINE_SPECIFICATION;
            Assert.NotNull(machine);

            var families = relatedElementMachine.T_FAMILY;
            Assert.NotNull(families);

            var editionClauses = relatedElementMachine.T_EDITION_CLAUSE;
            Assert.NotNull(editionClauses);


            var expectedMachine = MachineSpecification_Sample.GetMachineSpecification();
            Assert.Equal(expectedMachine.Code, machine.CODE);
            Assert.Equal(expectedMachine.AgeLimitAllowed, machine.AGE_LIMIT_ALLOWED);
            Assert.Equal(expectedMachine.AllPlacesCovered, machine.ALL_PLACE_CORVERED);
            Assert.Equal(expectedMachine.Description, machine.DESCRIPTION);
            Assert.Equal(expectedMachine.ExtendedFleetCoverageAllowedPercentage, machine.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE);
            Assert.Equal(expectedMachine.IsDelegated, machine.IS_DELEGATED);
            Assert.Equal(expectedMachine.IsUnreferenced, machine.IS_UNREFERENCED);
            Assert.Equal(expectedMachine.IsOutOfMachineInsurance, machine.IS_OUT_OF_MACHINE_INSURANCE);
            Assert.Equal(expectedMachine.IsExcluded, machine.IS_EXCLUDED);
            Assert.Equal(expectedMachine.IsTransportable, machine.IS_TRANSPORTABLE);
            Assert.Equal(string.Join("|", expectedMachine.Keywords), machine.KEYWORDS);
            Assert.Equal(expectedMachine.Label, machine.LABEL);
            Assert.Equal(expectedMachine.Name, machine.NAME);
            Assert.Equal(expectedMachine.MachineRate, machine.MACHINE_RATE);
            Assert.Equal(expectedMachine.Product, machine.PRODUCT);
            Assert.Equal(expectedMachine.Score, machine.SCORE);
            Assert.Equal(expectedMachine.SubscriptionPeriod.StartDateTime, machine.START_DATETIME_SUBSCRIPTION_PERIOD);
            Assert.Equal(expectedMachine.SubscriptionPeriod.EndDateTime, machine.END_DATETIME_SUBSCRIPTION_PERIOD);

            var expectedFamilies = T_FAMILY_Sample.GetDatabaseFamilies();
            Assert.NotEmpty(families);
            Assert.Equal(expectedFamilies.Count, families.Count());
            foreach (var expectedFamily in expectedFamilies)
            {
                var family = families.FirstOrDefault(x => x.CODE == expectedFamily.CODE && x.SUB_CODE == expectedFamily.SUB_CODE);
                Assert.NotNull(family);
                Assert.Equal(expectedFamily.NAME, family.NAME);
                Assert.Equal(expectedFamily.SUB_NAME, family.SUB_NAME);
                Assert.Equal(expectedFamily.SUB_CODE, family.SUB_CODE);
            }

            var expectedPricingRates = PricingRates_Sample.GetPricingRates();
            Assert.NotEmpty(machine.T_PRICING_RATE);
            Assert.Equal(expectedPricingRates.Count, machine.T_PRICING_RATE.Count);
            foreach (var expectedPricingRate in expectedPricingRates)
            {
                var pricingRate = machine.T_PRICING_RATE.FirstOrDefault(x => x.CODE == MapperPricingRate.ConvertPrincingCodeToDataBase(expectedPricingRate.Code));
                Assert.NotNull(pricingRate);
                Assert.Equal(expectedPricingRate.Rate, pricingRate.RATE);
            }

            var expectedEditionsClauses = EditionClauses_Sample.GetEditionClauses();
            Assert.NotEmpty(editionClauses);
            Assert.Equal(expectedEditionsClauses.Count, editionClauses.Count());
            foreach (var expectedEditionClause in expectedEditionsClauses)
            {
                var editionClause = editionClauses.FirstOrDefault(x => x.CODE == expectedEditionClause.Code);
                Assert.NotNull(editionClause);
                Assert.Equal(expectedEditionClause.Label, editionClause.LABEL);
                Assert.Equal(expectedEditionClause.Description, editionClause.DESCRIPTION);
                Assert.Equal(expectedEditionClause.Type, editionClause.TYPE);
            }
        }

        [Fact]
        public void Should_MapThrows_When_Input_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => _mapper.Map(null));
        }
    }
}
