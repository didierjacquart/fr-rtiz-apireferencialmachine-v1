using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using MC = BDM.ReferencialMachine.Core.Constants.MachineConstants;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Mappers
{
    public class MapperDatabaseToBusinessTests
    {
        private readonly Mock<IMapperFamilies> _mapperFamilies;
        private readonly Mock<IMapperPricingRate> _mapperPricingRates;
        private readonly Mock<IMapperEditionClauses> _mapperEditionClause;
        private readonly Mock<IMapperRiskPrecision> _mapperRiskPrecision;
        private readonly Fixture _fixture;


        private readonly IMapperDatabaseToBusiness _mapper;
        
        public MapperDatabaseToBusinessTests()
        {

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _mapperFamilies = new Mock<IMapperFamilies>();
            _mapperEditionClause = new Mock<IMapperEditionClauses>();
            _mapperPricingRates = new Mock<IMapperPricingRate>();
            _mapperRiskPrecision = new Mock<IMapperRiskPrecision>();
            _mapper = new MapperDatabaseToBusiness(_mapperPricingRates.Object, _mapperFamilies.Object, _mapperEditionClause.Object, _mapperRiskPrecision.Object);
        }


        [Fact]
        public void Should_MapDatabaseToMachineSpecification_When_Inventory_Nominal_Call()
        {
            //Arrange
            var tFamilies = new List<T_FAMILY>
            {
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    SUB_CODE = "SubCod_A",
                    SUB_NAME = "SubName_A",
                    CODE = "Code_A",
                    NAME = "Name_A"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    CODE = "Code_A",
                    NAME = "Name_A"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    CODE = "Code_B",
                    NAME = "Name_B"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    SUB_CODE = "SubCod_B1",
                    SUB_NAME = "SubName_B1",
                    CODE = "Code_B",
                    NAME = "Name_B"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    SUB_CODE = "SubCod_B2",
                    SUB_NAME = "SubName_B2",
                    CODE = "Code_B",
                    NAME = "Name_B"
                }
            };

            var tMachineSpecification = new T_MACHINE_SPECIFICATION
            {
                AGE_LIMIT_ALLOWED = 12,
                ALL_PLACE_CORVERED = "POINTLESS",
                CODE = "0010",
                DESCRIPTION = "Description",
                END_DATETIME_SUBSCRIPTION_PERIOD = null,
                START_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Now,
                EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE = 10,
                IS_OUT_OF_MACHINE_INSURANCE = false,
                IS_DELEGATED = true,
                IS_UNREFERENCED = false,
                IS_EXCLUDED = false,
                KEYWORDS = "keyword1|keyword2",
                LABEL = "Label",
                NAME = "Name",
                T_FAMILY = null,
                MACHINE_RATE = 1.2m,
                PRODUCT = "INVENTAIRE", 
                SCORE = "1",
                T_PRICING_RATE = null
            };

            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: "Should_MapDatabaseToMachineSpecification_Inventory_Nominal_Call")
                .Options;

            using var context = new MachineContext(options);
            context.AddRange(tFamilies);
            context.Add(tMachineSpecification);
            context.SaveChanges();
            
            //Act
            var result = _mapper.Map(tMachineSpecification, null);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(tMachineSpecification.AGE_LIMIT_ALLOWED, result.AgeLimitAllowed);
            Assert.Equal(tMachineSpecification.ALL_PLACE_CORVERED, result.AllPlacesCovered);
            Assert.Equal(tMachineSpecification.CODE, result.Code);
            Assert.Equal(tMachineSpecification.DESCRIPTION, result.Description);
            Assert.Equal(tMachineSpecification.END_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.EndDateTime);
            Assert.Equal(tMachineSpecification.START_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.StartDateTime);
            Assert.Equal(tMachineSpecification.IS_DELEGATED, result.IsDelegated);
            Assert.Equal(tMachineSpecification.IS_EXCLUDED, result.IsExcluded);
            Assert.Equal(tMachineSpecification.IS_UNREFERENCED, result.IsUnreferenced);
            Assert.Equal(tMachineSpecification.IS_OUT_OF_MACHINE_INSURANCE, result.IsOutOfMachineInsurance);
            Assert.Equal(tMachineSpecification.IS_TRANSPORTABLE, result.IsTransportable);
            Assert.Equal(tMachineSpecification.KEYWORDS, string.Join("|", result.Keywords));
            Assert.Equal(tMachineSpecification.LABEL, result.Label);
            Assert.Equal(tMachineSpecification.NAME, result.Name);
            Assert.Equal(tMachineSpecification.PRODUCT, result.Product);

        }

        [Fact]
        public void Should_MapDatabaseToMachineSpecification_When_PV_Nominal_Call()
        {
            //Arrange
            var tMachineSpecification = _fixture.Build<T_MACHINE_SPECIFICATION>().Create();
            tMachineSpecification.PRODUCT = MC.PHOTOVOLTAIC;
            
            var tEditionsClause = _fixture.Build<T_EDITION_CLAUSE>().CreateMany(3).ToList();
            tMachineSpecification.EDITION_CLAUSE_CODES = $"{tEditionsClause[0].CODE}|{tEditionsClause[1].CODE}|{tEditionsClause[2].CODE}";

            var tRiskPrecision = _fixture.Build<T_RISK_PRECISION>().CreateMany(3).ToList();
            tRiskPrecision.ForEach(x => x.MACHINE_CODE = tMachineSpecification.CODE);

            var tPricingRates = _fixture.Build<T_PRICING_RATE>().CreateMany(3).ToList();
            tPricingRates.ForEach(x => x.FK_MACHINE_ID = tMachineSpecification.MACHINE_ID);

            var expectedRiskPrecisions = _fixture.Build<RiskPrecision>().CreateMany(3).ToList();
            _mapperRiskPrecision.Setup(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>())).Returns(expectedRiskPrecisions);

            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: "Should_MapDatabaseToMachineSpecification_PV_Nominal_Call")
                .Options;

            var expectedClauses = _fixture.Build<EditionClause>().CreateMany(3).ToList();
            _mapperEditionClause.Setup(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>())).Returns(expectedClauses);

            var expectedPricingRates = _fixture.Build<PricingRate>().CreateMany(3).ToList();
            _mapperPricingRates.Setup(x => x.Map(It.IsAny<IEnumerable<T_PRICING_RATE>>())).Returns(expectedPricingRates);

            using var context = new MachineContext(options);

            context.Add(tMachineSpecification);
            context.AddRange(tPricingRates);
            context.AddRange(tRiskPrecision);
            context.AddRange(tEditionsClause);
            context.SaveChanges();

            //Act
            var result = _mapper.Map(tMachineSpecification, tEditionsClause, tRiskPrecision);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(tMachineSpecification.AGE_LIMIT_ALLOWED, result.AgeLimitAllowed);
            Assert.Equal(tMachineSpecification.ALL_PLACE_CORVERED, result.AllPlacesCovered);
            Assert.Equal(tMachineSpecification.CODE, result.Code);
            Assert.Equal(tMachineSpecification.DESCRIPTION, result.Description);
            Assert.Equal(tMachineSpecification.END_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.EndDateTime);
            Assert.Equal(tMachineSpecification.START_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.StartDateTime);
            Assert.Equal(tMachineSpecification.IS_DELEGATED, result.IsDelegated);
            Assert.Equal(tMachineSpecification.IS_EXCLUDED, result.IsExcluded);
            Assert.Equal(tMachineSpecification.IS_UNREFERENCED, result.IsUnreferenced);
            Assert.Equal(tMachineSpecification.IS_OUT_OF_MACHINE_INSURANCE, result.IsOutOfMachineInsurance);
            Assert.Equal(tMachineSpecification.IS_TRANSPORTABLE, result.IsTransportable);
            Assert.Equal(tMachineSpecification.KEYWORDS, string.Join("|", result.Keywords));
            Assert.Equal(tMachineSpecification.LABEL, result.Label);
            Assert.Equal(tMachineSpecification.NAME, result.Name);
            Assert.Equal(tMachineSpecification.PRODUCT, result.Product);
            Assert.Null(result.FinancialValuationMachineDelegatedMax);
            Assert.Null(result.FinancialValuationMachinedMax);


            Assert.Equal(expectedPricingRates, result.PricingRates);
            Assert.Null(result.RiskPrecisions);
            Assert.Equal(expectedClauses, result.EditionClauses);
            Assert.NotNull(result.AgreementDeductible);

            _mapperEditionClause.Verify(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()), Times.Once());
            _mapperPricingRates.Verify(x => x.Map(It.IsAny<IEnumerable<T_PRICING_RATE>>()), Times.Once());
            _mapperRiskPrecision.Verify(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Never()); 

        }

        [Fact]
        public void Should_MapDatabaseToLstMachineLight_Nominal_Call()
        {
            //Arrange
            var tFamilies = new List<T_FAMILY>
            {
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    SUB_CODE = "SubCod_A",
                    SUB_NAME = "SubName_A",
                    CODE = "Code_A",
                    NAME = "Name_A"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    CODE = "Code_A",
                    NAME = "Name_A"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    CODE = "Code_B",
                    NAME = "Name_B"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    SUB_CODE = "SubCod_B1",
                    SUB_NAME = "SubName_B1",
                    CODE = "Code_B",
                    NAME = "Name_B"
                },
                new T_FAMILY
                {
                    FAMILY_ID = new Guid(),
                    SUB_CODE = "SubCod_B2",
                    SUB_NAME = "SubName_B2",
                    CODE = "Code_B",
                    NAME = "Name_B"
                }
            };

            var tMachineSpecification = new T_MACHINE_SPECIFICATION
            {
                CODE = "0010",
                DESCRIPTION = "Description",
                IS_DELEGATED = true,
                IS_UNREFERENCED = false,
                IS_EXCLUDED = false,
                IS_OUT_OF_MACHINE_INSURANCE = false,
                KEYWORDS = "keyword1|keyword2",
                LABEL = "Label",
                NAME = "Name",
                SCORE = "1"
            };

            var lstTMachineSpecification = new List<T_MACHINE_SPECIFICATION>
            {
                tMachineSpecification, 
                tMachineSpecification
            };

            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: "Should_MapDatabaseToLstMachineLight_Nominal_Call")
                .Options;

            using var context = new MachineContext(options);
            context.T_FAMILY.AddRange(tFamilies);
            context.T_MACHINE_SPECIFICATION.AddRange(lstTMachineSpecification);
            context.SaveChanges();
            
            //Act
            var lstMMachineLight = _mapper.Map(lstTMachineSpecification, null);


            //Assert
            Assert.NotNull(lstMMachineLight);
            Assert.NotEmpty(lstMMachineLight);
            foreach (var machineLight in lstMMachineLight)
            {
                Assert.Equal(tMachineSpecification.CODE, machineLight.Code);
                Assert.Equal(tMachineSpecification.DESCRIPTION, machineLight.Description);
                Assert.Equal(tMachineSpecification.IS_DELEGATED, machineLight.IsDelegated);
                Assert.Equal(tMachineSpecification.IS_EXCLUDED, machineLight.IsExcluded);
                Assert.Equal(tMachineSpecification.IS_UNREFERENCED, machineLight.IsUnreferenced);
                Assert.Equal(tMachineSpecification.KEYWORDS, string.Join("|", machineLight.Keywords));
                Assert.Equal(tMachineSpecification.LABEL, machineLight.Label);
                Assert.Equal(tMachineSpecification.NAME, machineLight.Name);
                Assert.Equal(tMachineSpecification.SCORE, machineLight.Score);
            }
        }

        [Fact]
        public void Should_MapDatabaseToMachineSpecification_When_Park_Nominal_Call()
        {
            //Arrange
            var tMachineSpecification = _fixture.Build<T_MACHINE_SPECIFICATION>().Create();
            tMachineSpecification.PRODUCT = MC.PARK;

            var tEditionsClause = _fixture.Build<T_EDITION_CLAUSE>().CreateMany(3).ToList();
            tMachineSpecification.EDITION_CLAUSE_CODES = $"{tEditionsClause[0].CODE}|{tEditionsClause[1].CODE}|{tEditionsClause[2].CODE}";

            var tRiskPrecision = _fixture.Build<T_RISK_PRECISION>().CreateMany(3).ToList();
            tRiskPrecision.ForEach(x => x.MACHINE_CODE = tMachineSpecification.CODE);

            var tPricingRates = _fixture.Build<T_PRICING_RATE>().CreateMany(3).ToList();
            tPricingRates.ForEach(x => x.FK_MACHINE_ID = tMachineSpecification.MACHINE_ID);

            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: "Should_MapDatabaseToMachineSpecification_Park_Nominal_Call")
                .Options;

            var expectedClauses = _fixture.Build<EditionClause>().CreateMany(3).ToList();
            _mapperEditionClause.Setup(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>())).Returns(expectedClauses);

            var expectedPricingRates = _fixture.Build<PricingRate>().CreateMany(3).ToList();
            _mapperPricingRates.Setup(x => x.Map(It.IsAny<IEnumerable<T_PRICING_RATE>>())).Returns(expectedPricingRates);

            var expectedRiskPrecisions = _fixture.Build<RiskPrecision>().CreateMany(3).ToList();
            _mapperRiskPrecision.Setup(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>())).Returns(expectedRiskPrecisions);

            using var context = new MachineContext(options);

            context.Add(tMachineSpecification);
            context.AddRange(tPricingRates);
            context.AddRange(tRiskPrecision);
            context.AddRange(tEditionsClause);
            context.SaveChanges();

            //Act
            var result = _mapper.Map(tMachineSpecification, tEditionsClause, tRiskPrecision);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(tMachineSpecification.AGE_LIMIT_ALLOWED, result.AgeLimitAllowed);
            Assert.Equal(tMachineSpecification.ALL_PLACE_CORVERED, result.AllPlacesCovered);
            Assert.Equal(tMachineSpecification.CODE, result.Code);
            Assert.Equal(tMachineSpecification.DESCRIPTION, result.Description);
            Assert.Equal(tMachineSpecification.END_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.EndDateTime);
            Assert.Equal(tMachineSpecification.START_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.StartDateTime);
            Assert.Equal(tMachineSpecification.IS_DELEGATED, result.IsDelegated);
            Assert.Equal(tMachineSpecification.IS_EXCLUDED, result.IsExcluded);
            Assert.Equal(tMachineSpecification.IS_UNREFERENCED, result.IsUnreferenced);
            Assert.Equal(tMachineSpecification.IS_OUT_OF_MACHINE_INSURANCE, result.IsOutOfMachineInsurance);
            Assert.Equal(tMachineSpecification.IS_TRANSPORTABLE, result.IsTransportable);
            Assert.Equal(tMachineSpecification.KEYWORDS, string.Join("|", result.Keywords));
            Assert.Equal(tMachineSpecification.LABEL, result.Label);
            Assert.Equal(tMachineSpecification.NAME, result.Name);
            Assert.Equal(tMachineSpecification.PRODUCT, result.Product);
            Assert.Null(result.FinancialValuationMachineDelegatedMax);
            Assert.Null(result.FinancialValuationMachinedMax);

            Assert.Equal(expectedPricingRates, result.PricingRates);
            Assert.Equal(expectedRiskPrecisions, result.RiskPrecisions);
            Assert.Equal(expectedClauses, result.EditionClauses);
            Assert.NotNull(result.AgreementDeductible);

            _mapperEditionClause.Verify(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()), Times.Once());
            _mapperPricingRates.Verify(x => x.Map(It.IsAny<IEnumerable<T_PRICING_RATE>>()), Times.Once());
            _mapperRiskPrecision.Verify(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once());

        }

        [Fact]
        public void Should_MapDatabaseToMachineSpecification_When_RentalMachine_Nominal_Call()
        {
            //Arrange
            var tMachineSpecification = _fixture.Build<T_MACHINE_SPECIFICATION>().Create();
            tMachineSpecification.PRODUCT = MC.RENTAL_MACHINE;

            var tEditionsClause = _fixture.Build<T_EDITION_CLAUSE>().CreateMany(3).ToList();
            tMachineSpecification.EDITION_CLAUSE_CODES = $"{tEditionsClause[0].CODE}|{tEditionsClause[1].CODE}|{tEditionsClause[2].CODE}";

            var tRiskPrecision = _fixture.Build<T_RISK_PRECISION>().CreateMany(3).ToList();
            tRiskPrecision.ForEach(x => x.MACHINE_CODE = tMachineSpecification.CODE);

            var tPricingRates = _fixture.Build<T_PRICING_RATE>().CreateMany(3).ToList();
            tPricingRates.ForEach(x => x.FK_MACHINE_ID = tMachineSpecification.MACHINE_ID);

            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: "Should_MapDatabaseToMachineSpecification_RentalMachine_Nominal_Call")
                .Options;

            var expectedClauses = _fixture.Build<EditionClause>().CreateMany(3).ToList();
            _mapperEditionClause.Setup(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>())).Returns(expectedClauses);

            var expectedPricingRates = _fixture.Build<PricingRate>().CreateMany(3).ToList();
            _mapperPricingRates.Setup(x => x.Map(It.IsAny<IEnumerable<T_PRICING_RATE>>())).Returns(expectedPricingRates);

            var expectedRiskPrecisions = _fixture.Build<RiskPrecision>().CreateMany(3).ToList();
            _mapperRiskPrecision.Setup(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>())).Returns(expectedRiskPrecisions);

            using var context = new MachineContext(options);

            context.Add(tMachineSpecification);
            context.AddRange(tPricingRates);
            context.AddRange(tRiskPrecision);
            context.AddRange(tEditionsClause);
            context.SaveChanges();

            //Act
            var result = _mapper.Map(tMachineSpecification, tEditionsClause, tRiskPrecision);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(tMachineSpecification.AGE_LIMIT_ALLOWED, result.AgeLimitAllowed);
            Assert.Equal(tMachineSpecification.ALL_PLACE_CORVERED, result.AllPlacesCovered);
            Assert.Equal(tMachineSpecification.CODE, result.Code);
            Assert.Equal(tMachineSpecification.DESCRIPTION, result.Description);
            Assert.Equal(tMachineSpecification.END_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.EndDateTime);
            Assert.Equal(tMachineSpecification.START_DATETIME_SUBSCRIPTION_PERIOD, result.SubscriptionPeriod.StartDateTime);
            Assert.Equal(tMachineSpecification.IS_DELEGATED, result.IsDelegated);
            Assert.Equal(tMachineSpecification.IS_EXCLUDED, result.IsExcluded);
            Assert.Equal(tMachineSpecification.IS_UNREFERENCED, result.IsUnreferenced);
            Assert.Equal(tMachineSpecification.IS_OUT_OF_MACHINE_INSURANCE, result.IsOutOfMachineInsurance);
            Assert.Equal(tMachineSpecification.IS_TRANSPORTABLE, result.IsTransportable);
            Assert.Equal(tMachineSpecification.KEYWORDS, string.Join("|", result.Keywords));
            Assert.Equal(tMachineSpecification.LABEL, result.Label);
            Assert.Equal(tMachineSpecification.NAME, result.Name);
            Assert.Equal(tMachineSpecification.PRODUCT, result.Product);
            Assert.Equal(tMachineSpecification.FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX, result.FinancialValuationMachineDelegatedMax?.Amount);
            Assert.Equal(tMachineSpecification.FINANCIAL_VALUATION_MACHINE_MAX, result.FinancialValuationMachinedMax?.Amount);
            Assert.Equal(tMachineSpecification.FINANCIAL_VALUATION_MACHINE_CURRENCY, result.FinancialValuationMachineDelegatedMax?.CurrencyCode);
            Assert.Equal(tMachineSpecification.FINANCIAL_VALUATION_MACHINE_CURRENCY, result.FinancialValuationMachinedMax?.CurrencyCode);

            Assert.Equal(expectedPricingRates, result.PricingRates);
            Assert.Equal(expectedRiskPrecisions, result.RiskPrecisions);
            Assert.Equal(expectedClauses, result.EditionClauses);
            Assert.NotNull(result.AgreementDeductible);

            _mapperEditionClause.Verify(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()), Times.Once());
            _mapperPricingRates.Verify(x => x.Map(It.IsAny<IEnumerable<T_PRICING_RATE>>()), Times.Once());
            _mapperRiskPrecision.Verify(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once());

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_NotMap_When_Keywords_Null_Or_Empty(string keywords)
        {
            var tMachine = new T_MACHINE_SPECIFICATION {KEYWORDS = keywords};
            
            var result = _mapper.Map(tMachine, new List<T_RISK_PRECISION>());

            Assert.NotNull(result);
            Assert.Null(result.Keywords);
        }

        [Fact]
        public void Should_NotMap_When_Null_Values()
        {
            var tMachine = new T_MACHINE_SPECIFICATION();

            var result = _mapper.Map(tMachine, new List<T_RISK_PRECISION>());

            Assert.NotNull(result);
            Assert.Null(result.SubscriptionPeriod);
            Assert.Null(result.PricingRates);
        }

        [Fact]
        public void Should_Map_Throws_When_InputMachineIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _mapper.Map((T_MACHINE_SPECIFICATION)null, new List<T_RISK_PRECISION>()));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Should_Map_Throws_When_InputLstMachineIsNull()
        {
            var exception =
                Assert.Throws<ArgumentNullException>(() => _mapper.Map((List<T_MACHINE_SPECIFICATION>)null!, new List<T_RISK_PRECISION>()));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Should_NotMap_When_MachineSpecification_Null()
        {
            var machineSpecification = _mapper.Map(null, new List<T_EDITION_CLAUSE>(), new List<T_RISK_PRECISION>());

            Assert.Null(machineSpecification);
        }

        [Fact]
        public void Should_Map_When_EditionClauses_Null()
        {
            var machineSpecification = _mapper.Map(new T_MACHINE_SPECIFICATION{ CODE = "1"}, new List<T_RISK_PRECISION>());

            Assert.NotNull(machineSpecification);
            Assert.Equal("1", machineSpecification.Code);
            Assert.Null(machineSpecification.EditionClauses);
        }


        [Fact]
        public void Should_MapDatabaseToMachineSpecificationList_Nominal_Call()
        {
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(2);
            var machines = MachineBuilder.FakeDbMachine(dbClauses).Generate(2);
            
            var familiy1 = machines[0].T_FAMILY.First();
            var familiy2 = machines[1].T_FAMILY.First();

            _mapperFamilies.SetupSequence(x => x.Map(It.IsAny<List<T_FAMILY>>()))
                .Returns(new List<Family>
                {
                    new Family
                    {
                        Code = familiy1.CODE,
                        Name = familiy1.NAME,
                        SubFamilies = new List<SubFamily>
                        {
                            new SubFamily { Code = familiy1.SUB_CODE, Name = familiy1.SUB_NAME }
                        }
                    }
                })
                .Returns(new List<Family>
                {
                    new Family
                    {
                        Code = familiy2.CODE,
                        Name = familiy2.NAME,
                        SubFamilies = new List<SubFamily>
                        {
                            new SubFamily { Code = familiy2.SUB_CODE, Name = familiy2.SUB_NAME }
                        }
                    }
                });

            var pricingRate1 = machines[0].T_PRICING_RATE;
            var pricingRateList1 = new List<PricingRate>();
            foreach (var dbPricingRate in pricingRate1)
            {
                var pricingRate = new PricingRate { Code = MapperPricingRate.ConvertPrincingCodeToBusiness(dbPricingRate.CODE), Rate = dbPricingRate.RATE };
                pricingRateList1.Add(pricingRate);
            }

            var pricingRateList2 = new List<PricingRate>();
            foreach (var dbPricingRate in pricingRate1)
            {
                var pricingRate = new PricingRate { Code = MapperPricingRate.ConvertPrincingCodeToBusiness(dbPricingRate.CODE), Rate = dbPricingRate.RATE };
                pricingRateList2.Add(pricingRate);
            }

            _mapperPricingRates.SetupSequence(x => x.Map(It.IsAny<List<T_PRICING_RATE>>()))
                .Returns(pricingRateList1)
                .Returns(pricingRateList2);

            var machineSpecifications = _mapper.Map(machines, null);

            Assert.NotNull(machineSpecifications);

            foreach (var machine in machines)
            {
                var machineResult = machineSpecifications.FirstOrDefault(x => x.Code == machine.CODE);
                Assert.NotNull(machineResult);

                Assert.Equal(machine.AGE_LIMIT_ALLOWED, machineResult.AgeLimitAllowed);
                Assert.Equal(machine.ALL_PLACE_CORVERED, machineResult.AllPlacesCovered);
                Assert.Equal(machine.CODE, machineResult.Code);
                Assert.Equal(machine.DESCRIPTION, machineResult.Description);
                Assert.Equal(machine.END_DATETIME_SUBSCRIPTION_PERIOD, machineResult.SubscriptionPeriod.EndDateTime);
                Assert.Equal(machine.START_DATETIME_SUBSCRIPTION_PERIOD, machineResult.SubscriptionPeriod.StartDateTime);
                Assert.Equal(machine.IS_DELEGATED, machineResult.IsDelegated);
                Assert.Equal(machine.IS_EXCLUDED, machineResult.IsExcluded);
                Assert.Equal(machine.IS_UNREFERENCED, machineResult.IsUnreferenced);
                Assert.Equal(machine.IS_OUT_OF_MACHINE_INSURANCE, machineResult.IsOutOfMachineInsurance);
                Assert.Equal(machine.IS_TRANSPORTABLE, machineResult.IsTransportable);
                Assert.Equal(machine.KEYWORDS, string.Join("|", machineResult.Keywords));
                Assert.Equal(machine.LABEL, machineResult.Label);
                Assert.Equal(machine.NAME, machineResult.Name);
                Assert.Equal(machine.PRODUCT, machineResult.Product);

                var familyResult = machineResult.Families.FirstOrDefault();
                Assert.NotNull(familyResult);
                var expectedFamily = machine.T_FAMILY.FirstOrDefault();

                Assert.Equal(expectedFamily!.CODE, familyResult.Code);
                Assert.Equal(expectedFamily.NAME, familyResult.Name);
                Assert.Equal(expectedFamily.SUB_NAME, familyResult.SubFamilies.FirstOrDefault()?.Name);
                Assert.Equal(expectedFamily.SUB_CODE, familyResult.SubFamilies.FirstOrDefault()?.Code);

                var pricingResults = machineResult.PricingRates;
                Assert.NotNull(pricingResults);
                var expectedPricingResults = machine.T_PRICING_RATE;
                foreach (var expected in expectedPricingResults)
                {
                    var pricingResult = pricingResults.FirstOrDefault(x => x.Code == MapperPricingRate.ConvertPrincingCodeToBusiness(expected.CODE) && x.Rate == expected.RATE);
                    Assert.NotNull(pricingResult);

                }
            }
        }
        [Fact]
        public void Should_MapDatabaseToMachineSpecificationList_Nominal_When_PARK()
        {
            //Arrange
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(2);
            var park = MachineBuilder.FakeDbMachine(dbClauses).Generate();
            park.PRODUCT = MC.PARK;
            
            var expectedRiskPrecisions = _fixture.Build<RiskPrecision>().CreateMany(5);
            expectedRiskPrecisions = expectedRiskPrecisions.Select(x => new RiskPrecision()
            {
                Code = x.Code,
                Detail = x.Detail,
                Label = x.Label,
                MachineCode = park.CODE
            }).ToList();
            
            var pricingRates = park.T_PRICING_RATE;
            var pricingRateList = pricingRates
                .Select(dbPricingRate => 
                    new PricingRate
                    {
                        Code = MapperPricingRate.ConvertPrincingCodeToBusiness(dbPricingRate.CODE), 
                        Rate = dbPricingRate.RATE
                    }).ToList();


            _mapperPricingRates.Setup(x => x.Map(It.IsAny<List<T_PRICING_RATE>>()))
                .Returns(pricingRateList);

            _mapperRiskPrecision.Setup(x => x.Map(It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns((ICollection<RiskPrecision>)expectedRiskPrecisions);

            //Act
            var parkResult = _mapper.Map(park, new List<T_RISK_PRECISION>
            {
                new() { MACHINE_CODE = park.CODE }
            });

            //Assert
            Assert.NotNull(parkResult);
            
            Assert.Equal(park.AGE_LIMIT_ALLOWED, parkResult.AgeLimitAllowed);
            Assert.Equal(park.ALL_PLACE_CORVERED, parkResult.AllPlacesCovered);
            Assert.Equal(park.CODE, parkResult.Code);
            Assert.Equal(park.DESCRIPTION, parkResult.Description);
            Assert.Equal(park.END_DATETIME_SUBSCRIPTION_PERIOD, parkResult.SubscriptionPeriod.EndDateTime);
            Assert.Equal(park.START_DATETIME_SUBSCRIPTION_PERIOD, parkResult.SubscriptionPeriod.StartDateTime);
            Assert.Equal(park.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE, parkResult.ExtendedFleetCoverageAllowedPercentage);
            Assert.Equal(park.IS_DELEGATED, parkResult.IsDelegated);
            Assert.Equal(park.IS_EXCLUDED, parkResult.IsExcluded);
            Assert.Equal(park.IS_UNREFERENCED, parkResult.IsUnreferenced);
            Assert.Equal(park.IS_OUT_OF_MACHINE_INSURANCE, parkResult.IsOutOfMachineInsurance);
            Assert.Equal(park.IS_TRANSPORTABLE, parkResult.IsTransportable);
            Assert.Equal(park.KEYWORDS, string.Join("|", parkResult.Keywords));
            Assert.Equal(park.LABEL, parkResult.Label);
            Assert.Equal(park.NAME, parkResult.Name);
            Assert.Equal(park.PRODUCT, parkResult.Product);
            Assert.Equal(park.DEDUCTIBLE_MAX_VALUE, parkResult.AgreementDeductible.DeductibleInterval.MaximumValue);
            Assert.Equal(park.DEDUCTIBLE_MAX_INCLUDE, parkResult.AgreementDeductible.DeductibleInterval.MaximumInclude);
            Assert.Equal(park.DEDUCTIBLE_MIN_VALUE, parkResult.AgreementDeductible.DeductibleInterval.MinimumValue);
            Assert.Equal(park.DEDUCTIBLE_UNIT, parkResult.AgreementDeductible.DeductibleInterval.Unit);
            Assert.Equal(park.DEDUCTIBLE_MIN_INCLUDE, parkResult.AgreementDeductible.DeductibleInterval.MinimumInclude);
            Assert.Equal(park.IS_FIRE_COVERAGE_MANDATORY, parkResult.IsFireCoverageMandatory);
            Assert.Equal(park.FINANCIAL_VALUATION_DELEGATED_MAX, parkResult.FinancialValuationDelegatedMax);

            
            var pricingResults = parkResult.PricingRates;
            Assert.NotNull(pricingResults);
            var expectedPricingResults = park.T_PRICING_RATE;
            foreach (var expected in expectedPricingResults)
            {
                var pricingResult = pricingResults.FirstOrDefault(x => x.Code == MapperPricingRate.ConvertPrincingCodeToBusiness(expected.CODE) && x.Rate == expected.RATE);
                Assert.NotNull(pricingResult);
            }
            
            Assert.Equal(5, parkResult.RiskPrecisions.Count);
            Assert.Equal(expectedRiskPrecisions.First().Code, parkResult.RiskPrecisions.FirstOrDefault().Code);
            Assert.Equal(expectedRiskPrecisions.First().Label, parkResult.RiskPrecisions.FirstOrDefault().Label);
            Assert.Equal(expectedRiskPrecisions.First().Detail, parkResult.RiskPrecisions.FirstOrDefault().Detail);
            Assert.Equal(expectedRiskPrecisions.First().MachineCode, parkResult.Code);
        }
    }
}
