using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.Core.Services;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.Core.UnitTest.Services
{
    public class FamilyServiceTests
    {
        private Mock<IFamilyRepository> _familyRepository;
        public FamilyServiceTests()
        {
            _familyRepository = new Mock<IFamilyRepository>();
        }

        [Fact]
        public async Task Should_ReadFamiliesAsync_Returns_CollectionFamilies_When_Nominal_Call()
        {
            _familyRepository.Setup(x => x.ReadFamiliesAsync()).ReturnsAsync(new Collection<Family>());

            var familyService = new FamilyService(_familyRepository.Object);
            var response = await familyService.ReadFamiliesAsync();

            Assert.IsType<Collection<Family>>(response);
            _familyRepository.Verify(x => x.ReadFamiliesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Create_Throws_When_RepositoryCreate_Throws()
        {
            _familyRepository.Setup(x => x.CreateFamilyAsync(It.IsAny<Family>()))
                .ThrowsAsync(new Exception("exception"));

            var familyService = new FamilyService(_familyRepository.Object);
            var exception = await Assert.ThrowsAsync<Exception>(() => familyService.CreateFamilyAsync(new Family
            {
                Code = "code",
                Name = "name"
            }));

            Assert.Equal("exception", exception.Message);
            _familyRepository.Verify(x => x.CreateFamilyAsync(It.IsAny<Family>()), Times.Once);
        }

        [Theory]
        [InlineData("CODE", null)]
        [InlineData(null, "NAME")]
        public async Task Should_Create_Throws_When_Family_NotWellFormed(string code, string name)
        {
            var familyService = new FamilyService(_familyRepository.Object);
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => familyService.CreateFamilyAsync(new Family
            {
                Code = code,
                Name = name
            }));

            Assert.Equal(ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_MESSAGE, exception.Message);
            _familyRepository.Verify(x => x.CreateFamilyAsync(It.IsAny<Family>()), Times.Never);
        }

        [Fact]
        public async Task Should_Create_When_Nominal_Call()
        {
            var familyService = new FamilyService(_familyRepository.Object);
            await familyService.CreateFamilyAsync(new Family
            {
                Code = "code",
                Name = "name"
            });
            _familyRepository.Verify(x => x.CreateFamilyAsync(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_When_Nominal_Call()
        {
            var familyService = new FamilyService(_familyRepository.Object);
            
            await familyService.DeleteFamilyAsync("code");

            _familyRepository.Verify(x => x.DeleteFamilyAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Throws_When_RepositoryThrows()
        {

            _familyRepository.Setup(x => x.DeleteFamilyAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("exception"));

            var familyService = new FamilyService(_familyRepository.Object);
            var exception = await Assert.ThrowsAsync<Exception>(() => familyService.DeleteFamilyAsync("code"));
            
            Assert.Equal("exception", exception.Message);
            _familyRepository.Verify(x => x.DeleteFamilyAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Update_When_Nominal_Call()
        {
            var familyService = new FamilyService(_familyRepository.Object);
            await familyService.UpdateFamilyAsync("code", new Family
            {
                Code = "code",
                Name = "name"
            });
            _familyRepository.Verify(x => x.UpdateFamilyAsync(It.IsAny<string>(), It.IsAny<Family>()), Times.Once);

        }

        [Fact]
        public async Task Should_Update_Throws_When_RepositoryThrows()
        {
            _familyRepository.Setup(x => x.UpdateFamilyAsync(It.IsAny<string>(), It.IsAny<Family>()))
                .ThrowsAsync(new Exception("exception"));

            var familyService = new FamilyService(_familyRepository.Object);
            var exception = await Assert.ThrowsAsync<Exception>(() => familyService.UpdateFamilyAsync("code", new Family
            {
                Code = "code",
                Name = "name"
            }));

            Assert.Equal("exception", exception.Message);
            _familyRepository.Verify(x => x.UpdateFamilyAsync(It.IsAny<string>(), It.IsAny<Family>()), Times.Once);
        }


        [Theory]
        [InlineData("CODE", null)]
        [InlineData(null, "NAME")]
        public async Task Should_Update_Throws_When_Family_NotWellFormed(string code, string name)
        {
            var familyService = new FamilyService(_familyRepository.Object);
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => familyService.UpdateFamilyAsync("code", new Family
            {
                Code = code,
                Name = name
            }));

            Assert.Equal(ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_MESSAGE, exception.Message);
            _familyRepository.Verify(x => x.UpdateFamilyAsync(It.IsAny<string>(), It.IsAny<Family>()), Times.Never);
        }
    }
}
