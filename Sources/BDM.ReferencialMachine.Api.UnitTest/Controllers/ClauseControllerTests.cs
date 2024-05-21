using System.Threading.Tasks;
using BDM.ReferencialMachine.Api.Configuration;
using BDM.ReferencialMachine.Api.Controllers;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.Api.UnitTest.Controllers
{
    public class ClauseControllerTests
    {
        private readonly Mock<IClauseService> _clauseService;
        private readonly ClauseController _clauseController;
        private readonly MachinesCriteriasOptions _machineCriteriasOptions;

        public ClauseControllerTests()
        {
            _clauseService = new Mock<IClauseService>();
            _machineCriteriasOptions ??= new MachinesCriteriasOptions();
            var option = Options.Create(_machineCriteriasOptions);
            _clauseController = new ClauseController(_clauseService.Object);

        }

        [Fact]
        public async Task Should_Create_Clause_Returns_Ok()
        {
            //Act
            var response = await _clauseController.CreateClauseAsync(new EditionClause());

            //Assert
            Assert.IsType<CreatedResult>(response);
            _clauseService.Verify(x => x.CreateClauseAsync(It.IsAny<EditionClause>()), Times.Once);
        }

        [Fact]
        public async Task Should_Read_Clause_Returns_Ok()
        {
            //Act
            var response = await _clauseController.ReadClausesAsync();

            //Assert
            Assert.IsType<ActionResult<EditionClause[]>>(response);
            _clauseService.Verify(x => x.ReadClausesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Update_Clause_Returns_Ok()
        {
            //Act
            var response = await _clauseController.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>());

            //Assert
            Assert.IsType<OkResult>(response);
            _clauseService.Verify(x => x.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Clause_Returns_Ok()
        {
            //Act
            var response = await _clauseController.DeleteClauseAsync(It.IsAny<string>());

            //Assert
            Assert.IsType<OkResult>(response);
            _clauseService.Verify(x => x.DeleteClauseAsync(It.IsAny<string>()), Times.Once);
        }

       
    }
}
