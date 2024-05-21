using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using AutoFixture;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.Repositories;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MC = BDM.ReferencialMachine.Core.Constants.MachineConstants;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Repositories
{
    public class MachineRepositoryTests
    {
        private readonly Mock<IMapperDatabaseToBusiness> _mapperDatabaseToBusiness;
        private readonly Mock<IMapperBusinessToDatabase> _mapperBusinessToDatabase;
        private readonly Mock<ILogger<MachineRepository>> _logger;
        private readonly MachineRepository _repository;
        private readonly MachineContext _context;
        private readonly Fixture _fixture;

        public MachineRepositoryTests()
        {
            _mapperDatabaseToBusiness = new Mock<IMapperDatabaseToBusiness>();
            _mapperBusinessToDatabase = new Mock<IMapperBusinessToDatabase>();
            _logger = new Mock<ILogger<MachineRepository>>();
            var options = new DbContextOptionsBuilder<MachineContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new MachineContext(options);
            _repository = new MachineRepository(_context, _mapperDatabaseToBusiness.Object, _mapperBusinessToDatabase.Object, Mock.Of<ILogger<MachineRepository>>());
            
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Should_ReadMachine_Throws_When_No_Machine_Found()
        {
            var machineCode = "Bob";
            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(new Collection<MachineSpecification>());
            
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _repository.ReadMachineAsync(machineCode));
            
            Assert.Equal(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_MESSAGE(machineCode), exception.Message);
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Never);
        }

        [Fact]
        public async Task Should_Returns_MachineSpecification_On_Nominal_Call()
        {
            //Arrange
            await _context.T_FAMILY.AddRangeAsync(T_FAMILY_Sample.GetDatabaseFamilies());
            await _context.T_EDITION_CLAUSE.AddRangeAsync(T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses());
            var expected = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification();
            await _context.T_MACHINE_SPECIFICATION.AddAsync(expected);
            await _context.SaveChangesAsync();
            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<T_MACHINE_SPECIFICATION>(), It.IsAny<IEnumerable<T_EDITION_CLAUSE>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>() ))
                .Returns(new MachineSpecification());

            var result = await _repository.ReadMachineAsync(expected.CODE);

            Assert.NotNull(result);
            Assert.IsType<MachineSpecification>(result);
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<T_MACHINE_SPECIFICATION>(), It.IsAny<IEnumerable<T_EDITION_CLAUSE>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);

        }

        [Fact]
        public async Task Should_CreateMachine_On_Nominal_Call()
        {
            // Arrange
            await _context.T_FAMILY.AddRangeAsync(T_FAMILY_Sample.GetDatabaseFamilies());
            await _context.T_EDITION_CLAUSE.AddRangeAsync(T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses());
            await _context.SaveChangesAsync();

            var expectedMachine = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification();
            var expectedFamilies = T_FAMILY_Sample.GetDatabaseFamilies();
            var exectedPricingRates = T_PRICING_RATE_Sample.GetDatabasePricingRates();
            var expectedEditionClauses = T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses();

            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                    {
                        T_MACHINE_SPECIFICATION = expectedMachine,
                        T_FAMILY = expectedFamilies,
                        T_EDITION_CLAUSE = expectedEditionClauses
                    });

            // Act
            var result = await _repository.CreateMachineAsync(new MachineSpecification());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.True(result > 0);

            var machineResult = await _context.T_MACHINE_SPECIFICATION.FirstOrDefaultAsync(x => x.CODE == expectedMachine.CODE);
            Assert.NotNull(machineResult);

            Assert.Equal(expectedMachine.IS_DELEGATED, machineResult.IS_DELEGATED);
            Assert.Equal(expectedMachine.AGE_LIMIT_ALLOWED, machineResult.AGE_LIMIT_ALLOWED);
            Assert.Equal(expectedMachine.ALL_PLACE_CORVERED, machineResult.ALL_PLACE_CORVERED);
            Assert.Equal(expectedMachine.CODE, machineResult.CODE);

            
            var pricingRatesResult = machineResult.T_PRICING_RATE;
            Assert.NotEmpty(pricingRatesResult);

            foreach (var exectedPricingRate in exectedPricingRates)
            {
                Assert.Equal(exectedPricingRate.RATE, pricingRatesResult.FirstOrDefault(x => x.CODE == exectedPricingRate.CODE).RATE);
            }
            _mapperBusinessToDatabase.Verify(x => x.Map(It.IsAny<MachineSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Should_CreateMachine_Throws_BadRequestException_No_Families_In_Database()
        {
            // Given
            _context.T_FAMILY = _context.Set<T_FAMILY>();
            await _context.T_EDITION_CLAUSE.AddRangeAsync(T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses());
            await _context.SaveChangesAsync();
            
            var expectedMachine = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification();
            var expectedFamilies = T_FAMILY_Sample.GetDatabaseFamilies();
            var expectedEditionClauses = T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses();
            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = expectedMachine,
                    T_FAMILY = expectedFamilies,
                    T_EDITION_CLAUSE = expectedEditionClauses
                });

            // When
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _repository.CreateMachineAsync(It.IsAny<MachineSpecification>()));

            // Then
            Assert.Equal(ExceptionConstants.NO_MATCHING_FAMILY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.NO_MATCHING_FAMILY_EXCEPTION_MESSAGE("Code_A", "Sub_Code_A"), exception.Message);
            _mapperBusinessToDatabase.Verify(x => x.Map(It.IsAny<MachineSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Should_CreateMachine_Throws_BadRequestException_No_EditionClause_In_Database()
        {
            // Given
            await _context.T_FAMILY.AddRangeAsync(T_FAMILY_Sample.GetDatabaseFamilies());
            _context.T_EDITION_CLAUSE = _context.Set<T_EDITION_CLAUSE>();
            await _context.SaveChangesAsync();

            var expectedMachine = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification();
            var expectedFamilies = T_FAMILY_Sample.GetDatabaseFamilies();
            var expectedEditionClauses = T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses();
            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = expectedMachine,
                    T_FAMILY = expectedFamilies,
                    T_EDITION_CLAUSE = expectedEditionClauses
                });

            // When
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _repository.CreateMachineAsync(It.IsAny<MachineSpecification>()));

            // Then
            Assert.Equal(ExceptionConstants.NO_MATCHING_CLAUSE_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.NO_MATCHING_CLAUSE_EXCEPTION_MESSAGE("CODE1"), exception.Message);
            _mapperBusinessToDatabase.Verify(x => x.Map(It.IsAny<MachineSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Should_CreateMachine_Throws_When_DBUpdateException()
        {
            // Arrange
            var options = new DbContextOptions<MachineContext>();
            var contextMock = new Mock<MachineContext>(options);

            contextMock.Setup(m => m.T_MACHINE_SPECIFICATION).Returns(new Mock<DbSet<T_MACHINE_SPECIFICATION>>().Object);
            contextMock.SetupSequence(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Throws(new DbUpdateException())
                .Throws(new DbUpdateException());


            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification(),
                    T_FAMILY = It.IsAny<ICollection<T_FAMILY>>(),
                    T_EDITION_CLAUSE = It.IsAny<ICollection<T_EDITION_CLAUSE>>()
                });

            var machineRepository = new MachineRepository(contextMock.Object, _mapperDatabaseToBusiness.Object, _mapperBusinessToDatabase.Object, _logger.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<TechnicalException>(() => machineRepository.CreateMachineAsync(MachineSpecification_Sample.GetMachineSpecification()));

           
            // Assert
            Assert.Equal(ExceptionConstants.DB_UPDATE_EXCEPTION_MESSAGE, exception.Message);
        }

        [Fact]
        public async Task Should_ReadAllMachinesAsync_Returns_LstMachines_On_Nominal_Call()
        {
            _context.T_MACHINE_SPECIFICATION.Add(T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification());
            await _context.SaveChangesAsync();

            var expected = new Collection<MachineSpecification> { new MachineSpecification() };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            var resultList = await _repository.ReadAllMachinesAsync(null);

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.IsType<Collection<MachineSpecification>>(resultList);
            Assert.Equal(expected.Count, resultList.Count);
            var machineResult = resultList.FirstOrDefault();
            Assert.Null(machineResult.Label);
            Assert.Null(machineResult.AllPlacesCovered);
            Assert.Null(machineResult.PricingRates);
            Assert.Null(machineResult.Product);
            Assert.Null(machineResult.SubscriptionPeriod);
            Assert.Null(machineResult.AgeLimitAllowed);
            Assert.Null(machineResult.MachineRate);
            Assert.Null(machineResult.ExtendedFleetCoverageAllowedPercentage);
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);
        }

        [Fact]
        public async Task Should_ReadAllMachinesAsync_ReturnsParkSpecification_When_Nominal()
        {
            //Arrange
            var park = _fixture.Build<T_MACHINE_SPECIFICATION>()
                .Without(x => x.END_DATETIME_SUBSCRIPTION_PERIOD)
                .Create();
            park.PRODUCT = MC.PARK;


            var dbRiskPrecisions = _fixture.CreateMany<T_RISK_PRECISION>(5).ToList();
            dbRiskPrecisions = dbRiskPrecisions.Select(x => new T_RISK_PRECISION
            {
                MACHINE_CODE = park.CODE,
                DETAIL = x.DETAIL,
                LABEL = x.LABEL,
                CODE = x.CODE
            }).ToList();

            await _context.AddAsync(park);
            await _context.AddRangeAsync(dbRiskPrecisions);
            await _context.SaveChangesAsync();

            var riskPrecisions = dbRiskPrecisions.Select(x => new RiskPrecision
            {
                Label = x.LABEL, Code = x.CODE, Detail = x.DETAIL
            }).ToArray();

            var expected = new Collection<MachineSpecification> { new()
            {
                Label = park.LABEL,
                Name = park.NAME,
                Code = park.CODE,
                Product = park.PRODUCT,
                IsDelegated = park.IS_DELEGATED,
                IsUnreferenced = park.IS_UNREFERENCED,
                IsExcluded = park.IS_EXCLUDED,
                IsOutOfMachineInsurance = park.IS_OUT_OF_MACHINE_INSURANCE,
                RiskPrecisions = riskPrecisions
            } };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            //Act
            var resultList = await _repository.ReadAllMachinesAsync(MC.PARK);

            //Assert
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.IsType<Collection<MachineSpecification>>(resultList);

            var parkResult = resultList.FirstOrDefault();
            Assert.Equal(expected.First().Label, parkResult.Label);
            Assert.Equal(expected.First().Name, parkResult.Name);
            Assert.Equal(expected.First().Code, parkResult.Code);
            Assert.Equal(expected.First().IsDelegated, parkResult.IsDelegated);
            Assert.Equal(expected.First().IsUnreferenced, parkResult.IsUnreferenced);
            Assert.Equal(expected.First().IsExcluded, parkResult.IsExcluded);
            Assert.Equal(expected.First().IsOutOfMachineInsurance, parkResult.IsOutOfMachineInsurance);
            Assert.Equal(expected.First().Product, parkResult.Product);

            Assert.Equal(dbRiskPrecisions.Count, parkResult.RiskPrecisions.Count);
            var resultRiskPrecision = parkResult.RiskPrecisions.FirstOrDefault();
            Assert.NotNull(resultRiskPrecision);
            Assert.Equal(expected.First().RiskPrecisions.First().Code, resultRiskPrecision.Code);
            Assert.Equal(expected.First().RiskPrecisions.First().Detail, resultRiskPrecision.Detail);
            Assert.Equal(expected.First().RiskPrecisions.First().Label, resultRiskPrecision.Label);
            Assert.Null(resultRiskPrecision.MachineCode);
            Assert.Null(parkResult.AgreementDeductible);
        }

        [Fact]
        public async Task Should_ReadAllMachinesAsync_Returns_Excluded_Machines_On_Nominal_Call()
        {
            var tMachine = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification();
            tMachine.IS_EXCLUDED = true;
            _context.T_MACHINE_SPECIFICATION.Add(tMachine);
            await _context.SaveChangesAsync();

            var expected = new Collection<MachineSpecification> { new MachineSpecification(), new MachineSpecification() };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            var result = await _repository.ReadAllMachinesAsync(null);

            Assert.NotNull(result);
            Assert.IsType<Collection<MachineSpecification>>(result);
            Assert.Equal(expected.Count, result.Count);
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);
        }


        [Fact]
        public async Task Should_Returns_EmptyLstMachines_WhenNothingInDataBase()
        {
            var result = await _repository.ReadAllMachinesAsync(null);

            Assert.NotNull(result);
            Assert.IsType<Collection<MachineSpecification>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task Should_UpdateMachine_On_Nominal_Call()
        {
            // Arrange
            const string machineCode = "Code";
            var expectedFamilies = new List<T_FAMILY>
            {
                new T_FAMILY {CODE = "Code_A", NAME = "Name_A", SUB_CODE = "Sub_Code_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B1", SUB_NAME = "Sub_Name_B1"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B2", SUB_NAME = "Sub_Name_B2"},
                new T_FAMILY {CODE = "Code_C", NAME = "Name_C", SUB_CODE = "ROOT", SUB_NAME = null}
            };
            var expectedEditionClauses = new List<T_EDITION_CLAUSE>
            {
                new T_EDITION_CLAUSE { CODE = "CODE1",LABEL = "LabelClause1", DESCRIPTION = "DescriptionClause1", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE2",LABEL = "LabelClause2", DESCRIPTION = "DescriptionClause2", TYPE = null },
                new T_EDITION_CLAUSE { CODE = "CODE3",LABEL = "LabelClause3", DESCRIPTION = "DescriptionClause3", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE4",LABEL = "LabelClause4", DESCRIPTION = "DescriptionClause4", TYPE = null }
            };

            await _context.T_FAMILY.AddRangeAsync(expectedFamilies);
            await _context.T_EDITION_CLAUSE.AddRangeAsync(expectedEditionClauses);
            await _context.T_MACHINE_SPECIFICATION.AddRangeAsync(T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification());
            await _context.SaveChangesAsync();

            var expectedPricingRates = new List<T_PRICING_RATE>
            {
                new T_PRICING_RATE {CODE = "ADE", RATE = 2.1m},
                new T_PRICING_RATE {CODE = "INC", RATE = 2.2m},
                new T_PRICING_RATE {CODE = "GTL", RATE = 2.3m},
                new T_PRICING_RATE {CODE = "BDI", RATE = 2.4m},
                new T_PRICING_RATE {CODE = "VOL", RATE = 2.5m}
            };

            var expectedMachine = new T_MACHINE_SPECIFICATION
            {
                LABEL = "Label_1",
                NAME = "Name",
                CODE = machineCode,
                DESCRIPTION = "Description_1",
                KEYWORDS = "Keywords1|Keywords2|Keywords3",
                IS_DELEGATED = false,
                IS_UNREFERENCED = true,
                IS_EXCLUDED = true,
                AGE_LIMIT_ALLOWED = 123,
                ALL_PLACE_CORVERED = "FORBIDDEN_1",
                EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE = 123,
                MACHINE_RATE = 23,
                PRODUCT = "INVENTAIRE_1",
                SCORE = "12",
                START_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Today.AddDays(1),
                END_DATETIME_SUBSCRIPTION_PERIOD = null,
                T_PRICING_RATE = expectedPricingRates,
                EDITION_CLAUSE_CODES = string.Join("|",expectedEditionClauses.Select(x => x.CODE))
            };

            expectedFamilies = expectedFamilies.Skip(1).ToList();
            expectedEditionClauses = expectedEditionClauses.Take(4).ToList();

            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = expectedMachine,
                    T_FAMILY = expectedFamilies,
                    T_EDITION_CLAUSE = expectedEditionClauses
                });

            // Act
            await _repository.UpdateMachineAsync(machineCode, new MachineSpecification());

            // Assert
            var machineResults = await _context.T_MACHINE_SPECIFICATION.Where(x => x.CODE == expectedMachine.CODE).ToArrayAsync();
            Assert.Single(machineResults);

            var machineResult = machineResults.FirstOrDefault();
            Assert.NotNull(machineResult);
            Assert.Equal(expectedMachine.IS_UNREFERENCED, machineResult.IS_UNREFERENCED);
            Assert.Equal(expectedMachine.IS_EXCLUDED, machineResult.IS_EXCLUDED);
            Assert.Equal(expectedMachine.IS_DELEGATED, machineResult.IS_DELEGATED);
            Assert.Equal(expectedMachine.END_DATETIME_SUBSCRIPTION_PERIOD, machineResult.END_DATETIME_SUBSCRIPTION_PERIOD);
            Assert.Equal(expectedMachine.START_DATETIME_SUBSCRIPTION_PERIOD, machineResult.START_DATETIME_SUBSCRIPTION_PERIOD);
            Assert.Equal(expectedMachine.DESCRIPTION, machineResult.DESCRIPTION);
            Assert.Equal(expectedMachine.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE, machineResult.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE);
            Assert.Equal(expectedMachine.KEYWORDS, machineResult.KEYWORDS);
            Assert.Equal(expectedMachine.LABEL, machineResult.LABEL);
            Assert.Equal(expectedMachine.NAME, machineResult.NAME);
            Assert.Equal(expectedMachine.SCORE, machineResult.SCORE);
            Assert.Equal(expectedMachine.PRODUCT, machineResult.PRODUCT);
            Assert.Equal(expectedMachine.MACHINE_RATE, machineResult.MACHINE_RATE);
            Assert.Equal(expectedMachine.AGE_LIMIT_ALLOWED, machineResult.AGE_LIMIT_ALLOWED);
            Assert.Equal(expectedMachine.ALL_PLACE_CORVERED, machineResult.ALL_PLACE_CORVERED);
            Assert.Equal(expectedMachine.CODE, machineResult.CODE);
            
            var pricingRatesResult = machineResult.T_PRICING_RATE;
            Assert.NotEmpty(pricingRatesResult);
            Assert.Equal(expectedPricingRates.Count, pricingRatesResult.Count);
            foreach (var exectedPricingRate in expectedPricingRates)
            {
                Assert.Equal(exectedPricingRate.RATE, pricingRatesResult.FirstOrDefault(x => x.CODE == exectedPricingRate.CODE).RATE);
            }
            var familiesResult = machineResult.T_FAMILY;
            Assert.NotEmpty(familiesResult);
            Assert.Equal(expectedFamilies.Count, familiesResult.Count);
            foreach (var expectedFamily in expectedFamilies)
            {
                var familiResult = familiesResult.FirstOrDefault(x => x.CODE == expectedFamily.CODE && x.SUB_CODE == expectedFamily.SUB_CODE);
                Assert.NotNull(familiResult);
                Assert.Equal(expectedFamily.NAME, familiResult.NAME);
                Assert.Equal(expectedFamily.SUB_NAME, familiResult.SUB_NAME);
            }
            _mapperBusinessToDatabase.Verify(x => x.Map(It.IsAny<MachineSpecification>()), Times.Once);

            var editionClauseResults = _context.T_EDITION_CLAUSE.Where(x => expectedEditionClauses.Select(y => y.CODE).Contains(x.CODE));
            Assert.NotEmpty(editionClauseResults);
            Assert.Equal(expectedEditionClauses.Count, editionClauseResults.Count());
            foreach (var expectedEditionCLause in expectedEditionClauses)
            {
                var editionClauseResult = editionClauseResults.FirstOrDefault(x => x.CODE == expectedEditionCLause.CODE);
                Assert.NotNull(editionClauseResult);
                Assert.Equal(expectedEditionCLause.LABEL, editionClauseResult.LABEL);
                Assert.Equal(expectedEditionCLause.DESCRIPTION, editionClauseResult.DESCRIPTION);
                Assert.Equal(expectedEditionCLause.TYPE, editionClauseResult.TYPE);
            }
            _mapperBusinessToDatabase.Verify(x => x.Map(It.IsAny<MachineSpecification>()), Times.Once);

            // Checking database state
            var families = await _context.T_FAMILY.ToArrayAsync();
            Assert.Equal(8, families.Length);

            var editionClause = await _context.T_EDITION_CLAUSE.ToArrayAsync();
            Assert.Equal(4, editionClause.Length);

            var pricingRates = await _context.T_PRICING_RATE.ToArrayAsync();
            Assert.Equal(5, pricingRates.Length);

            var allMachineSpecifications = await _context.T_MACHINE_SPECIFICATION.ToArrayAsync();
            Assert.Single(allMachineSpecifications);
        }


        [Fact]
        public async Task Should_UpdateMachined_Not_Remove_Family()
        {
            // Arrange
            const string machineCode = "Code";
            var expectedFamilies = new List<T_FAMILY>
            {
                new T_FAMILY {CODE = "Code_A", NAME = "Name_A", SUB_CODE = "Sub_Code_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B1", SUB_NAME = "Sub_Name_B1"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B2", SUB_NAME = "Sub_Name_B2"},
                new T_FAMILY {CODE = "Code_C", NAME = "Name_C", SUB_CODE = "ROOT", SUB_NAME = null}
            };
            var expectedEditionClauses = new List<T_EDITION_CLAUSE>
            {
                new T_EDITION_CLAUSE { CODE = "CODE1",LABEL = "LabelClause1", DESCRIPTION = "DescriptionClause1", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE2",LABEL = "LabelClause2", DESCRIPTION = "DescriptionClause2", TYPE = null },
                new T_EDITION_CLAUSE { CODE = "CODE3",LABEL = "LabelClause3", DESCRIPTION = "DescriptionClause3", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE4",LABEL = "LabelClause4", DESCRIPTION = "DescriptionClause4", TYPE = null }
            };

            await _context.T_FAMILY.AddRangeAsync(expectedFamilies);
            await _context.T_EDITION_CLAUSE.AddRangeAsync(expectedEditionClauses);
            await _context.T_MACHINE_SPECIFICATION.AddRangeAsync(T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification());
            await _context.SaveChangesAsync();

            var expectedMachine = new T_MACHINE_SPECIFICATION
            {
                CODE = machineCode,
                END_DATETIME_SUBSCRIPTION_PERIOD = null
            };

            expectedFamilies = expectedFamilies.Skip(1).ToList();

            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = expectedMachine,
                    T_FAMILY = expectedFamilies,
                    T_EDITION_CLAUSE = expectedEditionClauses
                });

            // Act
            await _repository.UpdateMachineAsync(machineCode, new MachineSpecification());

            // Assert
            var machineResults = await _context.T_MACHINE_SPECIFICATION.Where(x => x.CODE == expectedMachine.CODE).ToArrayAsync();
            Assert.Single(machineResults);

            var families = await _context.T_FAMILY.ToArrayAsync();
            Assert.Equal(8, families.Length);
            
            var allMachineSpecifications = await _context.T_MACHINE_SPECIFICATION.ToArrayAsync();
            Assert.Single(allMachineSpecifications);
        }

        [Fact]
        public async Task Should_UpdateMachined_Not_Remove_EditionClauses()
        {
            // Arrange
            const string machineCode = "Code";
            var expectedFamilies = new List<T_FAMILY>
            {
                new T_FAMILY {CODE = "Code_A", NAME = "Name_A", SUB_CODE = "Sub_Code_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B1", SUB_NAME = "Sub_Name_B1"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B2", SUB_NAME = "Sub_Name_B2"},
                new T_FAMILY {CODE = "Code_C", NAME = "Name_C", SUB_CODE = "ROOT", SUB_NAME = null}
            };
            var expectedEditionClauses = new List<T_EDITION_CLAUSE>
            {
                new T_EDITION_CLAUSE { CODE = "CODE1",LABEL = "LabelClause1", DESCRIPTION = "DescriptionClause1", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE2",LABEL = "LabelClause2", DESCRIPTION = "DescriptionClause2", TYPE = null },
                new T_EDITION_CLAUSE { CODE = "CODE3",LABEL = "LabelClause3", DESCRIPTION = "DescriptionClause3", TYPE = "VOL" },
                new T_EDITION_CLAUSE { CODE = "CODE4",LABEL = "LabelClause4", DESCRIPTION = "DescriptionClause4", TYPE = null }
            };

            await _context.T_FAMILY.AddRangeAsync(expectedFamilies);
            await _context.T_EDITION_CLAUSE.AddRangeAsync(expectedEditionClauses);
            await _context.T_MACHINE_SPECIFICATION.AddRangeAsync(T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification());
            await _context.SaveChangesAsync();

            var expectedMachine = new T_MACHINE_SPECIFICATION
            {
                CODE = machineCode,
                END_DATETIME_SUBSCRIPTION_PERIOD = null
            };

            expectedFamilies = expectedFamilies.Skip(1).ToList();

            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = expectedMachine,
                    T_FAMILY = expectedFamilies,
                    T_EDITION_CLAUSE = expectedEditionClauses
                });

            // Act
            await _repository.UpdateMachineAsync(machineCode, new MachineSpecification());

            // Assert
            var machineResults = await _context.T_MACHINE_SPECIFICATION.Where(x => x.CODE == expectedMachine.CODE).ToArrayAsync();
            Assert.Single(machineResults);

            var editionClause = await _context.T_EDITION_CLAUSE.ToArrayAsync();
            Assert.Equal(4, editionClause.Length);

            var allMachineSpecifications = await _context.T_MACHINE_SPECIFICATION.ToArrayAsync();
            Assert.Single(allMachineSpecifications);
        }
        [Fact]
        public async Task Should_UpdateMachine_Throws_When_DBUpdateException()
        {
            var options = new DbContextOptions<MachineContext>();
            var context = new Mock<MachineContext>(options);

            context.Setup(x => x.T_MACHINE_SPECIFICATION).Throws<DbUpdateException>();

            var repository = new MachineRepository(context.Object, _mapperDatabaseToBusiness.Object, _mapperBusinessToDatabase.Object,
                _logger.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<TechnicalException>(() => repository.UpdateMachineAsync(It.IsAny<string>(),MachineSpecification_Sample.GetMachineSpecification()));

            //Assert
            Assert.Equal(ExceptionConstants.DB_UPDATE_EXCEPTION_MESSAGE, exception.Message);
        }

        [Fact]
        public async Task Should_UpdateMachine_Throws_When_No_Machine_Found()
        {
            var machineCode = "65021";

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(new Collection<MachineSpecification>());

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _repository.UpdateMachineAsync(machineCode, new MachineSpecification
                {
                    Code = machineCode
                }));

            Assert.Equal(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_MESSAGE(machineCode), exception.Message);
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Never);
        }

        [Fact]
        public async Task Should_UpdateMachine_Throws_When_MachineSpecificationIsNNull()
        {
            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(new Collection<MachineSpecification>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.UpdateMachineAsync(It.IsAny<string>(), null));

            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Never);
        }

        [Fact]
        public async Task Should_ReadMachinesListAsync_Returns_LstMachines_On_Nominal_Call()
        {
            var dbMachines = MachineBuilder.FakeDbMachine(null).Generate(2);

            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            _context.T_FAMILY.AddRange(dbMachines.SelectMany(x => x.T_FAMILY));
            await _context.SaveChangesAsync();

            var expected = new Collection<MachineSpecification> { new MachineSpecification(), new MachineSpecification() };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            var resultList = await _repository.ReadMachineListAsync(dbMachines.Select(x => x.CODE).ToArray());

            Assert.NotNull(resultList);
            Assert.Equal(expected.Count, resultList.Length);

            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);
        }

        [Fact]
        public async Task Should_ReadMachinesListAsync_ReturnsPark_When_NominalCall()
        {
            //Arrange
            var park = _fixture.Build<T_MACHINE_SPECIFICATION>()
                .Without(x => x.END_DATETIME_SUBSCRIPTION_PERIOD)
                .Create();
            park.PRODUCT = MC.PARK;


            var dbRiskPrecisions = _fixture.CreateMany<T_RISK_PRECISION>(5).ToList();
            dbRiskPrecisions = dbRiskPrecisions.Select(x => new T_RISK_PRECISION
            {
                MACHINE_CODE = park.CODE,
                DETAIL = x.DETAIL,
                LABEL = x.LABEL,
                CODE = x.CODE
            }).ToList();

            await _context.AddAsync(park);
            await _context.AddRangeAsync(dbRiskPrecisions);
            await _context.SaveChangesAsync();

            var riskPrecisions = dbRiskPrecisions.Select(x => new RiskPrecision
            {
                Label = x.LABEL,
                Code = x.CODE,
                Detail = x.DETAIL
            }).ToArray();

            var expected = new Collection<MachineSpecification> { new()
            {
                Label = park.LABEL,
                Name = park.NAME,
                Code = park.CODE,
                IsDelegated = park.IS_DELEGATED,
                IsUnreferenced = park.IS_UNREFERENCED,
                IsExcluded = park.IS_EXCLUDED,
                IsOutOfMachineInsurance = park.IS_OUT_OF_MACHINE_INSURANCE,
                RiskPrecisions = riskPrecisions,

                AgreementDeductible = new Deductible
                {
                    DeductibleInterval = new DeductibleInterval
                    {
                        MaximumValue = park.DEDUCTIBLE_MAX_VALUE,
                        MaximumInclude = park.DEDUCTIBLE_MAX_INCLUDE,
                        MinimumValue = park.DEDUCTIBLE_MIN_VALUE,
                        MinimumInclude = park.DEDUCTIBLE_MIN_INCLUDE,
                        Unit = park.DEDUCTIBLE_UNIT
                    }
                },
                AllPlacesCovered = park.ALL_PLACE_CORVERED,
                ExtendedFleetCoverageAllowedPercentage = park.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE,
                FinancialValuationDelegatedMax = park.FINANCIAL_VALUATION_DELEGATED_MAX,
                IsFireCoverageMandatory = park.IS_FIRE_COVERAGE_MANDATORY,
                IsTransportable = park.IS_TRANSPORTABLE,
                MachineRate = park.MACHINE_RATE,
                PricingRates = park.T_PRICING_RATE.Select(x => new PricingRate
                {
                    Code =  MapperPricingRate.ConvertPrincingCodeToBusiness("BDI"),
                    Rate = x.RATE
                }).ToArray(),
                Product = park.PRODUCT,
                SubscriptionPeriod = new TimePeriod { StartDateTime = park.START_DATETIME_SUBSCRIPTION_PERIOD},
                Score = park.SCORE
            } };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            //Act
            var resultList = await _repository.ReadMachineListAsync(new [] {park.CODE});

            //Assert
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.IsType<MachineSpecification[]>(resultList);
            Assert.Equal(expected.Count, resultList.Length);

            var parkResult = resultList.FirstOrDefault();
            Assert.Equal(expected.First().Label, parkResult.Label);
            Assert.Equal(expected.First().Name, parkResult.Name);
            Assert.Equal(expected.First().Code, parkResult.Code);
            Assert.Equal(expected.First().IsDelegated, parkResult.IsDelegated);
            Assert.Equal(expected.First().IsUnreferenced, parkResult.IsUnreferenced);
            Assert.Equal(expected.First().IsExcluded, parkResult.IsExcluded);
            Assert.Equal(expected.First().IsOutOfMachineInsurance, parkResult.IsOutOfMachineInsurance);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MinimumInclude, parkResult.AgreementDeductible.DeductibleInterval.MinimumInclude);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MinimumValue, parkResult.AgreementDeductible.DeductibleInterval.MinimumValue);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MaximumInclude, parkResult.AgreementDeductible.DeductibleInterval.MaximumInclude);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MaximumValue, parkResult.AgreementDeductible.DeductibleInterval.MaximumValue);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.Unit, parkResult.AgreementDeductible.DeductibleInterval.Unit);

            Assert.Equal(expected.First().AllPlacesCovered, parkResult.AllPlacesCovered);
            Assert.Equal(expected.First().ExtendedFleetCoverageAllowedPercentage, parkResult.ExtendedFleetCoverageAllowedPercentage);
            Assert.Equal(expected.First().FinancialValuationDelegatedMax, parkResult.FinancialValuationDelegatedMax);
            Assert.Equal(expected.First().IsFireCoverageMandatory, parkResult.IsFireCoverageMandatory);
            Assert.Equal(expected.First().IsTransportable, parkResult.IsTransportable);
            Assert.Equal(expected.First().MachineRate, parkResult.MachineRate);
            Assert.Equal(expected.First().Product, parkResult.Product);
            Assert.Equal(expected.First().Score, parkResult.Score);
            Assert.Equal(expected.First().SubscriptionPeriod.StartDateTime, parkResult.SubscriptionPeriod.StartDateTime);

            Assert.Equal(dbRiskPrecisions.Count, parkResult.RiskPrecisions.Count);
            var resultRiskPrecision = parkResult.RiskPrecisions.FirstOrDefault();
            Assert.NotNull(resultRiskPrecision);
            Assert.Equal(expected.First().RiskPrecisions.First().Code, resultRiskPrecision.Code);
            Assert.Equal(expected.First().RiskPrecisions.First().Detail, resultRiskPrecision.Detail);
            Assert.Equal(expected.First().RiskPrecisions.First().Label, resultRiskPrecision.Label);
            Assert.Null(resultRiskPrecision.MachineCode);

            Assert.Equal(expected.First().PricingRates.Count, parkResult.PricingRates.Count);

            var pricingRatesResult = parkResult.PricingRates.FirstOrDefault();
            Assert.NotNull(pricingRatesResult);

            Assert.Equal(expected.First().PricingRates.First().Rate, pricingRatesResult.Rate);
            Assert.Equal(expected.First().PricingRates.First().Code, pricingRatesResult.Code);

        }

        [Fact]
        public async Task Should_ReadMachinesListAsync_ReturnsPv_When_NominalCall()
        {
            //Arrange
            var pv = _fixture.Build<T_MACHINE_SPECIFICATION>()
                .Without(x => x.END_DATETIME_SUBSCRIPTION_PERIOD)
                .Create();
            pv.PRODUCT = MC.PHOTOVOLTAIC;

            // On PV RiskPrecisions is not set
            var dbRiskPrecisions = _fixture.CreateMany<T_RISK_PRECISION>(5).ToList();
            dbRiskPrecisions = dbRiskPrecisions.Select(x => new T_RISK_PRECISION
            {
                MACHINE_CODE = pv.CODE,
                DETAIL = x.DETAIL,
                LABEL = x.LABEL,
                CODE = x.CODE
            }).ToList();

            await _context.AddAsync(pv);
            await _context.AddRangeAsync(dbRiskPrecisions);
            await _context.SaveChangesAsync();

            var riskPrecisions = dbRiskPrecisions.Select(x => new RiskPrecision
            {
                Label = x.LABEL,
                Code = x.CODE,
                Detail = x.DETAIL
            }).ToArray();

            var expected = new Collection<MachineSpecification> { new()
            {
                Label = pv.LABEL,
                Name = pv.NAME,
                Code = pv.CODE,
                IsDelegated = pv.IS_DELEGATED,
                IsUnreferenced = pv.IS_UNREFERENCED,
                IsExcluded = pv.IS_EXCLUDED,
                IsOutOfMachineInsurance = pv.IS_OUT_OF_MACHINE_INSURANCE,

                AgreementDeductible = new Deductible
                {
                    DeductibleInterval = new DeductibleInterval
                    {
                        MaximumValue = pv.DEDUCTIBLE_MAX_VALUE,
                        MaximumInclude = pv.DEDUCTIBLE_MAX_INCLUDE,
                        MinimumValue = pv.DEDUCTIBLE_MIN_VALUE,
                        MinimumInclude = pv.DEDUCTIBLE_MIN_INCLUDE,
                        Unit = pv.DEDUCTIBLE_UNIT
                    }
                },
                AllPlacesCovered = pv.ALL_PLACE_CORVERED,
                ExtendedFleetCoverageAllowedPercentage = pv.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE,
                FinancialValuationDelegatedMax = pv.FINANCIAL_VALUATION_DELEGATED_MAX,
                IsFireCoverageMandatory = pv.IS_FIRE_COVERAGE_MANDATORY,
                IsTransportable = pv.IS_TRANSPORTABLE,
                MachineRate = pv.MACHINE_RATE,
                PricingRates = pv.T_PRICING_RATE.Select(x => new PricingRate
                {
                    Code =  MapperPricingRate.ConvertPrincingCodeToBusiness("BDI"),
                    Rate = x.RATE
                }).ToArray(),
                Product = pv.PRODUCT,
                SubscriptionPeriod = new TimePeriod { StartDateTime = pv.START_DATETIME_SUBSCRIPTION_PERIOD},
                Score = pv.SCORE
            } };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            //Act
            var resultList = await _repository.ReadMachineListAsync(new[] { pv.CODE });

            //Assert
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.IsType<MachineSpecification[]>(resultList);
            Assert.Equal(expected.Count, resultList.Length);

            var pvResult = resultList.FirstOrDefault();
            Assert.Equal(expected.First().Label, pvResult.Label);
            Assert.Equal(expected.First().Name, pvResult.Name);
            Assert.Equal(expected.First().Code, pvResult.Code);
            Assert.Equal(expected.First().IsDelegated, pvResult.IsDelegated);
            Assert.Equal(expected.First().IsUnreferenced, pvResult.IsUnreferenced);
            Assert.Equal(expected.First().IsExcluded, pvResult.IsExcluded);
            Assert.Equal(expected.First().IsOutOfMachineInsurance, pvResult.IsOutOfMachineInsurance);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MinimumInclude, pvResult.AgreementDeductible.DeductibleInterval.MinimumInclude);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MinimumValue, pvResult.AgreementDeductible.DeductibleInterval.MinimumValue);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MaximumInclude, pvResult.AgreementDeductible.DeductibleInterval.MaximumInclude);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MaximumValue, pvResult.AgreementDeductible.DeductibleInterval.MaximumValue);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.Unit, pvResult.AgreementDeductible.DeductibleInterval.Unit);

            Assert.Equal(expected.First().AllPlacesCovered, pvResult.AllPlacesCovered);
            Assert.Equal(expected.First().ExtendedFleetCoverageAllowedPercentage, pvResult.ExtendedFleetCoverageAllowedPercentage);
            Assert.Equal(expected.First().FinancialValuationDelegatedMax, pvResult.FinancialValuationDelegatedMax);
            Assert.Equal(expected.First().IsFireCoverageMandatory, pvResult.IsFireCoverageMandatory);
            Assert.Equal(expected.First().IsTransportable, pvResult.IsTransportable);
            Assert.Equal(expected.First().MachineRate, pvResult.MachineRate);
            Assert.Equal(expected.First().Product, pvResult.Product);
            Assert.Equal(expected.First().Score, pvResult.Score);
            Assert.Equal(expected.First().SubscriptionPeriod.StartDateTime, pvResult.SubscriptionPeriod.StartDateTime);

            Assert.Null(pvResult.RiskPrecisions);

            Assert.Equal(expected.First().PricingRates.Count, pvResult.PricingRates.Count);

            var pricingRatesResult = pvResult.PricingRates.FirstOrDefault();
            Assert.NotNull(pricingRatesResult);

            Assert.Equal(expected.First().PricingRates.First().Rate, pricingRatesResult.Rate);
            Assert.Equal(expected.First().PricingRates.First().Code, pricingRatesResult.Code);
        }

        [Fact]
        public async Task Should_ReadMachinesListAsync_ReturnsRentalMachine_When_NominalCall()
        {
            //Arrange
            var rentalMachine = _fixture.Build<T_MACHINE_SPECIFICATION>()
                .Without(x => x.END_DATETIME_SUBSCRIPTION_PERIOD)
                .Create();
            rentalMachine.PRODUCT = MC.RENTAL_MACHINE;

            // On PV RiskPrecisions is not set
            var dbRiskPrecisions = _fixture.CreateMany<T_RISK_PRECISION>(5).ToList();
            dbRiskPrecisions = dbRiskPrecisions.Select(x => new T_RISK_PRECISION
            {
                MACHINE_CODE = rentalMachine.CODE,
                DETAIL = x.DETAIL,
                LABEL = x.LABEL,
                CODE = x.CODE
            }).ToList();

            await _context.AddAsync(rentalMachine);
            await _context.AddRangeAsync(dbRiskPrecisions);
            await _context.SaveChangesAsync();

            var riskPrecisions = dbRiskPrecisions.Select(x => new RiskPrecision
            {
                Label = x.LABEL,
                Code = x.CODE,
                Detail = x.DETAIL
            }).AsEnumerable();

            var expected = new Collection<MachineSpecification> { new()
            {
                Label = rentalMachine.LABEL,
                Name = rentalMachine.NAME,
                Code = rentalMachine.CODE,
                IsDelegated = rentalMachine.IS_DELEGATED,
                IsUnreferenced = rentalMachine.IS_UNREFERENCED,
                IsExcluded = rentalMachine.IS_EXCLUDED,
                IsOutOfMachineInsurance = rentalMachine.IS_OUT_OF_MACHINE_INSURANCE,
                FinancialValuationMachinedMax = new CurrencyAmount
                {
                    Amount = rentalMachine.FINANCIAL_VALUATION_MACHINE_MAX,
                    CurrencyCode = rentalMachine.FINANCIAL_VALUATION_MACHINE_CURRENCY
                },
                FinancialValuationMachineDelegatedMax = new CurrencyAmount
                {
                    Amount = rentalMachine.FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX,
                    CurrencyCode = rentalMachine.FINANCIAL_VALUATION_MACHINE_CURRENCY
                },
                AgreementDeductible = new Deductible
                {
                    DeductibleInterval = new DeductibleInterval
                    {
                        MaximumValue = rentalMachine.DEDUCTIBLE_MAX_VALUE,
                        MaximumInclude = rentalMachine.DEDUCTIBLE_MAX_INCLUDE,
                        MinimumValue = rentalMachine.DEDUCTIBLE_MIN_VALUE,
                        MinimumInclude = rentalMachine.DEDUCTIBLE_MIN_INCLUDE,
                        Unit = rentalMachine.DEDUCTIBLE_UNIT
                    }
                },
                AllPlacesCovered = rentalMachine.ALL_PLACE_CORVERED,
                ExtendedFleetCoverageAllowedPercentage = rentalMachine.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE,
                FinancialValuationDelegatedMax = rentalMachine.FINANCIAL_VALUATION_DELEGATED_MAX,
                IsFireCoverageMandatory = rentalMachine.IS_FIRE_COVERAGE_MANDATORY,
                IsTransportable = rentalMachine.IS_TRANSPORTABLE,
                MachineRate = rentalMachine.MACHINE_RATE,
                PricingRates = rentalMachine.T_PRICING_RATE.Select(x => new PricingRate
                {
                    Code =  MapperPricingRate.ConvertPrincingCodeToBusiness("BDI"),
                    Rate = x.RATE
                }).ToArray(),
                RiskPrecisions = new MapperRiskPrecision().Map(dbRiskPrecisions),
                Product = rentalMachine.PRODUCT,
                SubscriptionPeriod = new TimePeriod { StartDateTime = rentalMachine.START_DATETIME_SUBSCRIPTION_PERIOD},
                Score = rentalMachine.SCORE
            } };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            //Act
            var resultList = await _repository.ReadMachineListAsync(new[] { rentalMachine.CODE });

            //Assert
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.IsType<MachineSpecification[]>(resultList);
            Assert.Equal(expected.Count, resultList.Length);

            var rentalResult = resultList.FirstOrDefault();
            Assert.Equal(expected.First().Label, rentalResult.Label);
            Assert.Equal(expected.First().Name, rentalResult.Name);
            Assert.Equal(expected.First().Code, rentalResult.Code);
            Assert.Equal(expected.First().IsDelegated, rentalResult.IsDelegated);
            Assert.Equal(expected.First().IsUnreferenced, rentalResult.IsUnreferenced);
            Assert.Equal(expected.First().IsExcluded, rentalResult.IsExcluded);
            Assert.Equal(expected.First().IsOutOfMachineInsurance, rentalResult.IsOutOfMachineInsurance);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MinimumInclude, rentalResult.AgreementDeductible.DeductibleInterval.MinimumInclude);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MinimumValue, rentalResult.AgreementDeductible.DeductibleInterval.MinimumValue);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MaximumInclude, rentalResult.AgreementDeductible.DeductibleInterval.MaximumInclude);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.MaximumValue, rentalResult.AgreementDeductible.DeductibleInterval.MaximumValue);
            Assert.Equal(expected.First().AgreementDeductible.DeductibleInterval.Unit, rentalResult.AgreementDeductible.DeductibleInterval.Unit);

            Assert.Equal(expected.First().AllPlacesCovered, rentalResult.AllPlacesCovered);
            Assert.Equal(expected.First().ExtendedFleetCoverageAllowedPercentage, rentalResult.ExtendedFleetCoverageAllowedPercentage);
            Assert.Equal(expected.First().FinancialValuationDelegatedMax, rentalResult.FinancialValuationDelegatedMax);
            Assert.Equal(expected.First().IsFireCoverageMandatory, rentalResult.IsFireCoverageMandatory);
            Assert.Equal(expected.First().IsTransportable, rentalResult.IsTransportable);
            Assert.Equal(expected.First().MachineRate, rentalResult.MachineRate);
            Assert.Equal(expected.First().Product, rentalResult.Product);
            Assert.Equal(expected.First().Score, rentalResult.Score);
            Assert.Equal(expected.First().SubscriptionPeriod.StartDateTime, rentalResult.SubscriptionPeriod.StartDateTime);
            
            Assert.Equal(expected.First().FinancialValuationMachinedMax.Amount, rentalResult.FinancialValuationMachinedMax.Amount);
            Assert.Equal(expected.First().FinancialValuationMachinedMax.CurrencyCode, rentalResult.FinancialValuationMachinedMax.CurrencyCode);
            Assert.Equal(expected.First().FinancialValuationMachineDelegatedMax.Amount, rentalResult.FinancialValuationMachineDelegatedMax.Amount);
            Assert.Equal(expected.First().FinancialValuationMachineDelegatedMax.CurrencyCode, rentalResult.FinancialValuationMachineDelegatedMax.CurrencyCode);

            Assert.NotEmpty(rentalResult.RiskPrecisions);
            var riskPrecisionResult = rentalResult.RiskPrecisions.FirstOrDefault();
            Assert.Equal(expected.First().RiskPrecisions.First().Code, riskPrecisionResult?.Code);
            Assert.Equal(expected.First().RiskPrecisions.First().Label, riskPrecisionResult?.Label);

            Assert.Equal(expected.First().PricingRates.Count, rentalResult.PricingRates.Count);

            var pricingRatesResult = rentalResult.PricingRates.FirstOrDefault();
            Assert.NotNull(pricingRatesResult);

            Assert.Equal(expected.First().PricingRates.First().Rate, pricingRatesResult.Rate);
            Assert.Equal(expected.First().PricingRates.First().Code, pricingRatesResult.Code);
        }

        [Fact]
        public async Task Should_ReadMachinesListAsync_Throws_When_A_Machine_Is_NotFound()
        {
            var dbMachines = MachineBuilder.FakeDbMachine(null).Generate(2);

            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            _context.T_FAMILY.AddRange(dbMachines.SelectMany(x => x.T_FAMILY));
            await _context.SaveChangesAsync();

            var expected = new Collection<MachineSpecification> { new MachineSpecification(), new MachineSpecification() };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _repository.ReadMachineListAsync(new []{"1", "2"}));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINES_NOT_FOUND_EXCEPTION_MESSAGE("1;2"), exception.Message);

            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<IEnumerable<T_MACHINE_SPECIFICATION>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Never);
        }

        [Fact]
        public async Task Should_ReadMachineAsync_ReturnsPark_When_NominalCall()
        {
            //Arrange
            var park = _fixture.Build<T_MACHINE_SPECIFICATION>()
                .Without(x => x.END_DATETIME_SUBSCRIPTION_PERIOD)
                .Create();
            park.PRODUCT = MC.PARK;


            var dbRiskPrecisions = _fixture.CreateMany<T_RISK_PRECISION>(5).ToList();
            dbRiskPrecisions = dbRiskPrecisions.Select(x => new T_RISK_PRECISION
            {
                MACHINE_CODE = park.CODE,
                DETAIL = x.DETAIL,
                LABEL = x.LABEL,
                CODE = x.CODE
            }).ToList();

            await _context.AddAsync(park);
            await _context.AddRangeAsync(dbRiskPrecisions);
            await _context.SaveChangesAsync();

            var riskPrecisions = dbRiskPrecisions.Select(x => new RiskPrecision
            {
                Label = x.LABEL,
                Code = x.CODE,
                Detail = x.DETAIL
            }).ToArray();

            var expected = new MachineSpecification 
            {
                Label = park.LABEL,
                Name = park.NAME,
                Code = park.CODE,
                IsDelegated = park.IS_DELEGATED,
                IsUnreferenced = park.IS_UNREFERENCED,
                IsExcluded = park.IS_EXCLUDED,
                IsOutOfMachineInsurance = park.IS_OUT_OF_MACHINE_INSURANCE,
                RiskPrecisions = riskPrecisions,

                AgreementDeductible = new Deductible
                {
                    DeductibleInterval = new DeductibleInterval
                    {
                        MaximumValue = park.DEDUCTIBLE_MAX_VALUE,
                        MaximumInclude = park.DEDUCTIBLE_MAX_INCLUDE,
                        MinimumValue = park.DEDUCTIBLE_MIN_VALUE,
                        MinimumInclude = park.DEDUCTIBLE_MIN_INCLUDE,
                        Unit = park.DEDUCTIBLE_UNIT
                    }
                },
                AllPlacesCovered = park.ALL_PLACE_CORVERED,
                ExtendedFleetCoverageAllowedPercentage = park.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE,
                FinancialValuationDelegatedMax = park.FINANCIAL_VALUATION_DELEGATED_MAX,
                IsFireCoverageMandatory = park.IS_FIRE_COVERAGE_MANDATORY,
                IsTransportable = park.IS_TRANSPORTABLE,
                MachineRate = park.MACHINE_RATE,
                PricingRates = park.T_PRICING_RATE.Select(x => new PricingRate
                {
                    Code =  MapperPricingRate.ConvertPrincingCodeToBusiness("BDI"),
                    Rate = x.RATE
                }).ToArray(),
                Product = park.PRODUCT,
                SubscriptionPeriod = new TimePeriod { StartDateTime = park.START_DATETIME_SUBSCRIPTION_PERIOD},
                Score = park.SCORE
            };

            _mapperDatabaseToBusiness
                .Setup(x => x.Map(It.IsAny<T_MACHINE_SPECIFICATION>(), It.IsAny<IEnumerable<T_EDITION_CLAUSE>>(), It.IsAny<IEnumerable<T_RISK_PRECISION>>()))
                .Returns(expected);


            //Act
            var parkSpecification = await _repository.ReadMachineAsync(park.CODE);

            //Assert
            _mapperDatabaseToBusiness.Verify(x => x.Map(It.IsAny<T_MACHINE_SPECIFICATION>(),It.IsAny<IEnumerable<T_EDITION_CLAUSE>>() ,It.IsAny<IEnumerable<T_RISK_PRECISION>>()), Times.Once);

            Assert.NotNull(parkSpecification);
            Assert.IsType<MachineSpecification>(parkSpecification);
            
            
            Assert.Equal(expected.Label, parkSpecification.Label);
            Assert.Equal(expected.Name, parkSpecification.Name);
            Assert.Equal(expected.Code, parkSpecification.Code);
            Assert.Equal(expected.IsDelegated, parkSpecification.IsDelegated);
            Assert.Equal(expected.IsUnreferenced, parkSpecification.IsUnreferenced);
            Assert.Equal(expected.IsExcluded, parkSpecification.IsExcluded);
            Assert.Equal(expected.IsOutOfMachineInsurance, parkSpecification.IsOutOfMachineInsurance);
            Assert.Equal(expected.AgreementDeductible.DeductibleInterval.MinimumInclude, parkSpecification.AgreementDeductible.DeductibleInterval.MinimumInclude);
            Assert.Equal(expected.AgreementDeductible.DeductibleInterval.MinimumValue, parkSpecification.AgreementDeductible.DeductibleInterval.MinimumValue);
            Assert.Equal(expected.AgreementDeductible.DeductibleInterval.MaximumInclude, parkSpecification.AgreementDeductible.DeductibleInterval.MaximumInclude);
            Assert.Equal(expected.AgreementDeductible.DeductibleInterval.MaximumValue, parkSpecification.AgreementDeductible.DeductibleInterval.MaximumValue);
            Assert.Equal(expected.AgreementDeductible.DeductibleInterval.Unit, parkSpecification.AgreementDeductible.DeductibleInterval.Unit);

            Assert.Equal(expected.AllPlacesCovered, parkSpecification.AllPlacesCovered);
            Assert.Equal(expected.ExtendedFleetCoverageAllowedPercentage, parkSpecification.ExtendedFleetCoverageAllowedPercentage);
            Assert.Equal(expected.FinancialValuationDelegatedMax, parkSpecification.FinancialValuationDelegatedMax);
            Assert.Equal(expected.IsFireCoverageMandatory, parkSpecification.IsFireCoverageMandatory);
            Assert.Equal(expected.IsTransportable, parkSpecification.IsTransportable);
            Assert.Equal(expected.MachineRate, parkSpecification.MachineRate);
            Assert.Equal(expected.Product, parkSpecification.Product);
            Assert.Equal(expected.Score, parkSpecification.Score);
            Assert.Equal(expected.SubscriptionPeriod.StartDateTime, parkSpecification.SubscriptionPeriod.StartDateTime);

            Assert.Equal(dbRiskPrecisions.Count, parkSpecification.RiskPrecisions.Count);
            var resultRiskPrecision = parkSpecification.RiskPrecisions.FirstOrDefault();
            Assert.NotNull(resultRiskPrecision);
            Assert.Equal(expected.RiskPrecisions.First().Code, resultRiskPrecision.Code);
            Assert.Equal(expected.RiskPrecisions.First().Detail, resultRiskPrecision.Detail);
            Assert.Equal(expected.RiskPrecisions.First().Label, resultRiskPrecision.Label);
            Assert.Null(resultRiskPrecision.MachineCode);

            Assert.Equal(expected.PricingRates.Count, parkSpecification.PricingRates.Count);

            var pricingRatesResult = parkSpecification.PricingRates.FirstOrDefault();
            Assert.NotNull(pricingRatesResult);

            Assert.Equal(expected.PricingRates.First().Rate, pricingRatesResult.Rate);
            Assert.Equal(expected.PricingRates.First().Code, pricingRatesResult.Code);

        }
    }
}
