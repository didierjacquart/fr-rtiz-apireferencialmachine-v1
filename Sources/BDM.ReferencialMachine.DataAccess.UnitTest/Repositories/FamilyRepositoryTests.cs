using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.Repositories;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Repositories
{
    public class FamilyRepositoryTests
    {
        private readonly Mock<IMapperFamilies> _mapperFamilies;
        private readonly Mock<ILogger<IFamilyRepository>> _logger;
        private readonly DbContextOptions<MachineContext> _options;

        public FamilyRepositoryTests()
        {
            _mapperFamilies = new Mock<IMapperFamilies>();
            _logger = new Mock<ILogger<IFamilyRepository>>();
            _options = new DbContextOptionsBuilder<MachineContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }
        
        [Fact]
        public async Task Should_Returns_Families_On_Nominal_Call()
        {
            await using var context = new MachineContext(_options);
            await context.T_FAMILY.AddRangeAsync(T_FAMILY_Sample.GetDatabaseFamilies());
            await context.SaveChangesAsync();

            var expected = MachineFamilies_Sample.GetMachineFamilies();

            _mapperFamilies
                .Setup(x => x.Map(It.IsAny<ICollection<T_FAMILY>>()))
                .Returns(expected);


            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);
            var result = await repository.ReadFamiliesAsync();

            Assert.NotNull(result);
            Assert.Equal(expected.Count, result.Count);
            _mapperFamilies.Verify(x => x.Map(It.IsAny<ICollection<T_FAMILY>>()), Times.Once);
        }

        [Fact]
        public async Task Should_Returns_EmptyLstFamilies_WhenNothingInDataBase()
        {
            // Arrange
            await using var context = new MachineContext(_options);
            var families = new Collection<Family>();
            _mapperFamilies
                .Setup(x => x.Map(It.IsAny<ICollection<T_FAMILY>>()))
                .Returns(families);


            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);
            var result = await repository.ReadFamiliesAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Should_ReadFamiliesAsync_Throws_When_DbException()
        {
            // Arrange
            var contextMock = new Mock<MachineContext>(_options);

            contextMock.Setup(m => m.T_FAMILY).Throws(new Exception());

            //Act
            var repository = new FamilyRepository(contextMock.Object, _mapperFamilies.Object, _logger.Object);
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.ReadFamiliesAsync());

            Assert.NotNull(exception);
            contextMock.Verify(m => m.T_FAMILY, Times.Once());
        }

        [Fact]
        public async Task Should_NotCreate_When_FamilyWithSameCodeExists()
        {
            var contextMock = new Mock<MachineContext>(_options);
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601).Build(); // Duplicate key
            var dbUpdateException = new DbUpdateException("Foo", sqlException);
            
            contextMock.Setup(m => m.T_FAMILY).Returns(new Mock<DbSet<T_FAMILY>>().Object);
            contextMock.Setup(m => m.SaveChangesAsync(default)).Throws(dbUpdateException);
            
            _mapperFamilies
                .Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(new [] { new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_A" }});

            var repository = new FamilyRepository(contextMock.Object, _mapperFamilies.Object, _logger.Object);
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => repository.CreateFamilyAsync(new Family{ Code = "Code_A" }));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.FAMILY_ALREADY_EXIST_EXCCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.FAMILY_ALREADY_EXIST_EXCCEPTION_MESSAGE("Code_A"), exception.Message);
            _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Create_When_Nominal_Call()
        {
            await using var context = new MachineContext(_options);

            await context.T_FAMILY.AddRangeAsync(T_FAMILY_Sample.GetDatabaseFamilies());
            await context.SaveChangesAsync();

            
            _mapperFamilies
                .Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(new[]
                {
                    new T_FAMILY{CODE = "code", SUB_CODE = "subcode", NAME = "name", SUB_NAME = "subname"}
                });

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);
            await repository.CreateFamilyAsync(new Family
            {
                Code = "code",
                Name = "name",
                SubFamilies = new List<SubFamily>
                {
                    new SubFamily
                    {
                        Name = "subname",
                        Code = "subcode"
                    }
                }
            });

            var resultInDb = await context.T_FAMILY.FirstOrDefaultAsync(x => x.CODE == "code" && x.SUB_CODE == "subcode");
            Assert.NotNull(resultInDb);
            Assert.Equal("name", resultInDb.NAME);
            Assert.Equal("subname", resultInDb.SUB_NAME);

            _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_When_Nominal_Call()
        {
            await using var context = new MachineContext(_options);

            await context.T_FAMILY.AddAsync(new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_A" });
            await context.T_FAMILY.AddAsync(new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_B" });
            await context.SaveChangesAsync();

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);
            await repository.DeleteFamilyAsync("Code_A");


            var resultInDb = await context.T_FAMILY.AnyAsync(x => x.CODE == "Code_A");
            Assert.False(resultInDb);
        }

        [Fact]
        public async Task Should_Delete_Throws_When_Family_Not_Found()
        {
            await using var context = new MachineContext(_options);

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => repository.DeleteFamilyAsync("Code_A"));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_MESSAGE("Code_A"), exception.Message);
        }

        [Fact]
        public async Task Should_Delete_Throws_When_Family_Attached_To_Machine()
        {
            await using var context = new MachineContext(_options);
            await context.T_MACHINE_SPECIFICATION.AddAsync(new T_MACHINE_SPECIFICATION
            {
                CODE = "A",
                T_FAMILY = new List<T_FAMILY>
                {
                    new T_FAMILY {CODE = "Code_A", SUB_CODE = "Sub_Code_A"},
                    new T_FAMILY {CODE = "Code_A", SUB_CODE = "Sub_Code_B"}
                }
            });
            await context.SaveChangesAsync();

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => repository.DeleteFamilyAsync("Code_A"));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_MESSAGE, exception.Message);
        }

        [Fact]
        public async Task Should_Update_Family_When_Nominal_Call()
        {
            await using var context = new MachineContext(_options);
            var dbFamilies = new []
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_2", NAME = "Name_A", SUB_NAME = "Sub_Name_B"}
            };
            await context.T_FAMILY.AddRangeAsync(dbFamilies);
            await context.SaveChangesAsync();

            var inputFamilies = new Family
            {
                Code = "Code_A",
                Name = "Name_1",
                SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Code = "Sub_Code_1", Name = "Sub_Name_1"},
                        new SubFamily {Code = "Sub_Code_2", Name = "Sub_Name_2"},
                    }
            };

            var inputConvertedFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_1", SUB_NAME = "Sub_Name_1"},
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_2", NAME = "Name_1", SUB_NAME = "Sub_Name_2"}
            };

            _mapperFamilies.Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(inputConvertedFamilies);

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);

            await repository.UpdateFamilyAsync("Code_A", inputFamilies);


            var resultInDb = await context.T_FAMILY.ToArrayAsync();
            Assert.Equal(inputConvertedFamilies.Length, resultInDb.Length);

            foreach (var inputConvertedFamily in inputConvertedFamilies)
            {
                var dbFamily = resultInDb.FirstOrDefault(x =>
                    x.CODE == inputConvertedFamily.CODE && x.SUB_CODE == inputConvertedFamily.SUB_CODE);

                Assert.Equal(inputConvertedFamily.NAME, dbFamily.NAME);
                Assert.Equal(inputConvertedFamily.SUB_NAME, dbFamily.SUB_NAME);
            }

            _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }
        [Fact]
        public async Task Should_Update_Family_RemoveSubFamilyNotSended()
        {
            await using var context = new MachineContext(_options);
            var dbFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_2", NAME = "Name_A", SUB_NAME = "Sub_Name_B"}
            };
            await context.T_FAMILY.AddRangeAsync(dbFamilies);
            await context.SaveChangesAsync();

            var inputFamilies = new Family
            {
                Code = "Code_A",
                Name = "Name_1",
                SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Code = "Sub_Code_1", Name = "Sub_Name_1"}
                    }
            };

            var inputConvertedFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_1", SUB_NAME = "Sub_Name_1"}
            };
            
            _mapperFamilies.Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(inputConvertedFamilies);

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);

            await repository.UpdateFamilyAsync("Code_A", inputFamilies);

            var resultInDb = await context.T_FAMILY.ToArrayAsync();
            Assert.Single(resultInDb);

            var dbFamily = resultInDb.FirstOrDefault(x => x.CODE == inputConvertedFamilies.FirstOrDefault().CODE && x.SUB_CODE == inputConvertedFamilies.FirstOrDefault().SUB_CODE);
            Assert.NotNull(dbFamily);
            Assert.Equal(inputConvertedFamilies.FirstOrDefault().NAME, dbFamily.NAME);
            Assert.Equal(inputConvertedFamilies.FirstOrDefault().SUB_NAME, dbFamily.SUB_NAME);
            
            _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Update_Family_Throws_Family_Not_Found()
        {
           await using var context = new MachineContext(_options);

            var inputFamilies = new Family
            {
                Code = "Code_A",
                Name = "Name_1",
                SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Code = "Sub_Code_1", Name = "Sub_Name_1"}
                    }
            };

            var inputConvertedFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_1", SUB_NAME = "Sub_Name_1"}
            };
            
            _mapperFamilies.Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(inputConvertedFamilies);

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);

            var exception =  await Assert.ThrowsAsync<NotFoundException>(() => repository.UpdateFamilyAsync("Code_A", inputFamilies));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.FAMILY_NOT_FOUND_EXCEPTION_MESSAGE("Code_A"), exception.Message);
            _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }
        
        [Fact]
        public async Task Should_Update_AddFamily_When_NotExistingInDb()
        {
            await using var context = new MachineContext(_options);
            var dbFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_A", SUB_NAME = "Sub_Name_A"}
            };
            await context.T_MACHINE_SPECIFICATION.AddAsync(new T_MACHINE_SPECIFICATION
            {
                CODE = "code",
                T_FAMILY = dbFamilies.ToList()
            });
            await context.SaveChangesAsync();

            var inputFamilies = new Family
            {
                Code = "Code_A",
                Name = "Name_1",
                SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Code = "Sub_Code_1", Name = "Sub_Name_1"},
                        new SubFamily {Code = "Sub_Code_2", Name = "Sub_Name_2"}
                    }
            };

            var inputConvertedFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_1", SUB_NAME = "Sub_Name_1"},
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_2", NAME = "Name_1", SUB_NAME = "Sub_Name_2"}
            };

            _mapperFamilies.Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(inputConvertedFamilies);

            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);

            await repository.UpdateFamilyAsync("Code_A", inputFamilies);

            var resultInDb = await context.T_FAMILY.ToArrayAsync();
            Assert.Equal(2, resultInDb.Length);
            
            Assert.True(resultInDb.FirstOrDefault(x => x.SUB_CODE == "Sub_Code_1").T_MACHINE_SPECIFICATION.Any(x => x.CODE=="code"));
            Assert.True(resultInDb.FirstOrDefault(x => x.SUB_CODE == "Sub_Code_2").T_MACHINE_SPECIFICATION == null);

            foreach (var family in inputConvertedFamilies)
            {
                var dbFamily = resultInDb.FirstOrDefault(x => x.CODE == family.CODE && x.SUB_CODE == family.SUB_CODE);            
                Assert.NotNull(dbFamily);
                Assert.Equal(family.NAME, dbFamily.NAME);
                Assert.Equal(family.SUB_NAME, dbFamily.SUB_NAME);
                
            }
            _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Update_Family_Throws_When_FamilyToBeDeleteAttachToAMachine()
        {
            await using var context = new MachineContext(_options);
            var dbFamilies = new List<T_FAMILY>
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_2", NAME = "Name_A", SUB_NAME = "Sub_Name_B"}
            };
            await context.T_MACHINE_SPECIFICATION.AddAsync(new T_MACHINE_SPECIFICATION
            {
                CODE = "code",
                T_FAMILY = dbFamilies
            });
            await context.SaveChangesAsync();

            var inputFamilies = new Family
            {
                Code = "Code_A",
                Name = "Name_1",
                SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Code = "Sub_Code_1", Name = "Sub_Name_1"}
                    }
            };

            var inputConvertedFamilies = new[]
            {
                new T_FAMILY { CODE = "Code_A", SUB_CODE = "Sub_Code_1", NAME = "Name_1", SUB_NAME = "Sub_Name_1"}
            };

            _mapperFamilies.Setup(x => x.Map(It.IsAny<Family>()))
                .Returns(inputConvertedFamilies);

            
            var repository = new FamilyRepository(context, _mapperFamilies.Object, _logger.Object);


            var exception = await Assert.ThrowsAsync<BadRequestException>(() => repository.UpdateFamilyAsync("Code_A", inputFamilies));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_MESSAGE, exception.Message);

           _mapperFamilies.Verify(x => x.Map(It.IsAny<Family>()), Times.Once);
        }
    }
}
