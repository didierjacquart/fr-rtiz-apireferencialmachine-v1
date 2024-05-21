using System;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Api.Controllers;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.Api.UnitTest.Controllers
{
    public class FamilyControllerTests
    {
        [Fact]
        public async Task Should_Read_Families_Returns_Ok()
        {
            var familyService = new Mock<IFamilyService>();
            var famillyController = new FamilyController(familyService.Object);

            var response = await famillyController.ReadFamiliesAsync();

            Assert.IsType<ActionResult<Family[]>>(response);
            familyService.Verify(x => x.ReadFamiliesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Create_Family_Returns_Ok()
        {
            var familyService = new Mock<IFamilyService>();
            var famillyController = new FamilyController(familyService.Object);

            var response = await famillyController.CreateFamilyAsync(new Family());

            Assert.IsType<CreatedResult>(response);
            familyService.Verify(x => x.CreateFamilyAsync(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Create_Family_Throws_When_ServiceThrows()
        {
            var familyService = new Mock<IFamilyService>();
            familyService.Setup(x => x.CreateFamilyAsync(It.IsAny<Family>())).ThrowsAsync(
                new Exception("exceptionMessage"));

            var famillyController = new FamilyController(familyService.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => famillyController.CreateFamilyAsync(new Family()));

            Assert.NotNull(exception);
            Assert.Equal("exceptionMessage", exception.Message);
            familyService.Verify(x => x.CreateFamilyAsync(It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Family_Returns_Ok()
        {
            var familyService = new Mock<IFamilyService>();
            var famillyController = new FamilyController(familyService.Object);

            var response = await famillyController.DeleteFamilyAsync(It.IsAny<string>());

            Assert.IsType<OkResult>(response);
            familyService.Verify(x => x.DeleteFamilyAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Family_Throws_WhenServiceThrows()
        {
            var familyService = new Mock<IFamilyService>();
            familyService.Setup(x => x.DeleteFamilyAsync(It.IsAny<string>())).ThrowsAsync(
                new Exception("mon exception"));


            var famillyController = new FamilyController(familyService.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => famillyController.DeleteFamilyAsync(It.IsAny<string>()));

            Assert.NotNull(exception);
            Assert.Equal("mon exception", exception.Message);
            familyService.Verify(x => x.DeleteFamilyAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Update_Family_Family_Returns_Ok()
        {
            var familyService = new Mock<IFamilyService>();
            var famillyController = new FamilyController(familyService.Object);

            var response = await famillyController.UpdateFamilyAsync("code", new Family());
            
            Assert.IsType<OkResult>(response);
            familyService.Verify(x => x.UpdateFamilyAsync( "code",It.IsAny<Family>()), Times.Once);
        }

        [Fact]
        public async Task Should_Update_Family_WhenServiceThrows()
        {
            var familyService = new Mock<IFamilyService>();
            familyService.Setup(x => x.UpdateFamilyAsync(It.IsAny<string>(), It.IsAny<Family>())).ThrowsAsync(
                new Exception("exceptionMessage"));
                    
            var famillyController = new FamilyController(familyService.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => famillyController.UpdateFamilyAsync(It.IsAny<string>(),It.IsAny<Family>()));

            Assert.NotNull(exception);
            Assert.Equal("exceptionMessage", exception.Message);
            familyService.Verify(x => x.UpdateFamilyAsync(It.IsAny<string>(),It.IsAny<Family>()), Times.Once);
        }
    }
}
