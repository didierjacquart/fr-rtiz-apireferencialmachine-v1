using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Api.Configuration;
using BDM.ReferencialMachine.Api.Controllers;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.Api.UnitTest.Controllers
{
    public class MachineControllerTests
    {
        private readonly MachineController _machineController;
        private readonly Mock<IMachineService> _machineService;
        private readonly MachinesCriteriasOptions _machineCriteriasOptions;

        public MachineControllerTests()
        {
            _machineService = new Mock<IMachineService>();
            _machineCriteriasOptions = new MachinesCriteriasOptions();
            var option = Options.Create(_machineCriteriasOptions);
            _machineController = new MachineController(_machineService.Object, option);
        }

        [Fact]
        public async Task Should_Create_Machine_Returns_CreatedResult()
        {
            var response = await _machineController.CreateMachineAsync(new MachineSpecification());

            Assert.IsType<CreatedResult>(response);
            _machineService.Verify(x => x.CreateMachineAsync(It.IsAny<MachineSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Should_Read_Machine_Returns_Ok()
        {
            var response = await _machineController.ReadMachineAsync(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(response);
            _machineService.Verify(x => x.ReadMachineAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_Update_Machine_Returns_Ok()
        {
            var response = await _machineController.UpdateMachineAsync(It.IsAny<string>(), It.IsAny<MachineSpecification>());

            Assert.IsType<OkResult>(response);
            _machineService.Verify(x => x.UpdateMachineAsync(It.IsAny<string>(), It.IsAny<MachineSpecification>()), Times.Once);
        }

        [Fact]
        public async Task Should_ReadAllMachine_Returns_ListMachineSpecification()
        {
            _machineService.Setup(x => x.ReadAllMachinesAsync(It.IsAny<string>())).ReturnsAsync(new Collection<MachineSpecification>
            {
                new MachineSpecification(), new MachineSpecification()
            });

            var response = await _machineController.ReadAllMachinesAsync(It.IsAny<string>());

            Assert.IsType<ActionResult<MachineSpecification[]>>(response);
            var machines = Assert.IsType<Collection<MachineSpecification>>(((ObjectResult)response.Result).Value);
            Assert.Equal(2, machines.Count);
            _machineService.Verify(x => x.ReadAllMachinesAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Should_ReadMachineList_Returns_ListMachineSpecification()
        {
            _machineService.Setup(x => x.ReadMachineListAsync(new[] { "02002", "02001" })).ReturnsAsync(new[]
            {
                new MachineSpecification(), new MachineSpecification()
            });

            var machinesCriterias = new MachineCriterias { MachineCodeList = new Collection<string> { "02002", "02001" } };

            var machineCriteriasOptions = new MachinesCriteriasOptions
            {
                MaxRequestedCodes = 5
            };
            var option = Options.Create(machineCriteriasOptions);
            var machineController = new MachineController(_machineService.Object, option);

            var response = await machineController.ReadMachineListAsync(machinesCriterias);

            Assert.IsType<ActionResult<MachineSpecification[]>>(response);
            var machines = Assert.IsType<MachineSpecification[]>(((ObjectResult)response.Result).Value);
            Assert.Equal(2, machines.Length);
            _machineService.Verify(x => x.ReadMachineListAsync(new[] { "02002", "02001" }), Times.Once);
        }

        [Fact]
        public async Task Should_ReadMachineList_Throws_When_NumberCodesSendeedExcedTheLimit()
        {
            _machineService.Setup(x => x.ReadMachineListAsync(new[] { "02002", "02001" })).ReturnsAsync(new[]
            {
                new MachineSpecification(), new MachineSpecification()
            });

            var machineCriteriasOptions = new MachinesCriteriasOptions
            {
                MaxRequestedCodes = 1
            };
            var option = Options.Create(machineCriteriasOptions);
            var machineController = new MachineController(_machineService.Object, option);

            var machinesCriterias = new MachineCriterias { MachineCodeList = new Collection<string> { "02002", "02001" } };

            var exception = await Assert.ThrowsAsync<RequestEntityTooLargeException>(async () => 
                await machineController.ReadMachineListAsync(machinesCriterias));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(1), exception.Message);

            _machineService.Verify(x => x.ReadMachineListAsync(new[] { "02002", "02001" }), Times.Never);
        }
        [Fact]
        public async Task Should_ReadMachineList_Throws_When_NumberCodesSendeedExcedTheDefaultLimit()
        {
            const int defaultMaxCodesRequested = 50;

            var machinesCriterias = new MachineCriterias { MachineCodeList = new Collection<string>() };
            for (int i = 0; i < defaultMaxCodesRequested + 1; i++)
            {
                machinesCriterias.MachineCodeList.Add("code_" + i);
            }

            var exception = await Assert.ThrowsAsync<RequestEntityTooLargeException>(() => _machineController.ReadMachineListAsync(machinesCriterias));

            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(defaultMaxCodesRequested), exception.Message);

            _machineService.Verify(x => x.ReadMachineListAsync(It.IsAny<string[]>()), Times.Never);
        }
        [Fact]
        public async Task Should_ReadMachineList_ThrowsArgumentNullExcpetion_When_MachineCriterias_Null()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _machineController.ReadMachineListAsync(null));

            Assert.NotNull(exception);

            _machineService.Verify(x => x.ReadMachineListAsync(It.IsAny<string[]>()), Times.Never);
        }
        [Fact]
        public async Task Should_ReadMachineList_ThrowsArgumentNullExcpetion_When_MachineCodeList_Null()
        {
            var machineCriterias = new MachineCriterias();

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _machineController.ReadMachineListAsync(machineCriterias));

            Assert.NotNull(exception);

            _machineService.Verify(x => x.ReadMachineListAsync(It.IsAny<string[]>()), Times.Never);
        }

        [Fact]
        public async Task Should_ReadAMachinesListAsync_Throws_When_Duplicated_Codes()
        {
            var machineCriterias = new MachineCriterias
            {
                MachineCodeList = new Collection<string>
                {
                    "1","1","1"
                }
            };

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _machineController.ReadMachineListAsync(machineCriterias));

            Assert.Equal(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE("1"), exception.Message);

            _machineService.Verify(x => x.ReadMachineListAsync(It.IsAny<string[]>()), Times.Never);
        }


        [Fact]
        public async Task Should_ReadMachineClausesListAsync_Returns_List_EditionClausesByMachine()
        {
            //Arrange
            _machineService.Setup(x => x.ReadMachineClausesListAsync(new[] { "02002", "02001" }))
                .ReturnsAsync(new List<EditionClausesByMachine>
                    {
                        new EditionClausesByMachine { MachineCode = "02002", EditionClauseList = new Collection<EditionClause>()},
                        new EditionClausesByMachine { MachineCode = "02001", EditionClauseList = new Collection<EditionClause>()}
                    }
                );
            var machinesCriterias = new MachineCriterias { MachineCodeList = new Collection<string> { "02002", "02001" } };

            //Act
            var response = await _machineController.ReadMachineClausesListAsync(machinesCriterias);

            //Assert
            var editionClausesByMachines = Assert.IsType<List<EditionClausesByMachine>>(((ObjectResult)response.Result).Value);
            Assert.Equal(2, editionClausesByMachines.Count);

            _machineService.Verify(x => x.ReadMachineClausesListAsync(new[] { "02002", "02001" }), Times.Once);
        }

        [Fact]
        public async Task Should_ReadMachineClausesListAsync_Throws_When_NumberCodesSendeedExcedTheLimit()
        {
            var machineCriteriasOptions = new MachinesCriteriasOptions
            {
                MaxRequestedCodes = 1
            };
            var option = Options.Create(machineCriteriasOptions);
            var machineController = new MachineController(_machineService.Object, option);

            var machinesCriterias = new MachineCriterias { MachineCodeList = new Collection<string> { "02002", "02001" } };

            //Act
            var exception = await Assert.ThrowsAsync<RequestEntityTooLargeException>(() => machineController.ReadMachineClausesListAsync(machinesCriterias));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(1), exception.Message);

            _machineService.Verify(x => x.ReadMachineClausesListAsync(new[] { "02002", "02001" }), Times.Never);
        }

        [Fact]
        public async Task Should_ReadMachineClausesListAsync_Throws_When_NumberCodesSendeedExcedTheDefaultLimit()
        {
            //Arrange
            const int defaultMaxCodesRequested = 50;

            var machinesCriterias = new MachineCriterias { MachineCodeList = new Collection<string>() };
            for (int i = 0; i < defaultMaxCodesRequested + 1; i++)
            {
                machinesCriterias.MachineCodeList.Add("code_" + i);
            }

            //Act
            var exception = await Assert.ThrowsAsync<RequestEntityTooLargeException>(() => _machineController.ReadMachineClausesListAsync(machinesCriterias));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(defaultMaxCodesRequested), exception.Message);

            _machineService.Verify(x => x.ReadMachineClausesListAsync(It.IsAny<string[]>()), Times.Never);
        }

        [Fact]
        public async Task Should_ReadMachineClausesListAsync_ThrowsArgumentNullExcpetion_When_MachineCriterias_Null()
        {
            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _machineController.ReadMachineClausesListAsync(null));

            //Assert
            Assert.NotNull(exception);
            _machineService.Verify(x => x.ReadMachineClausesListAsync(It.IsAny<string[]>()), Times.Never);
        }
        [Fact]
        public async Task Should_ReadMachineClausesListAsync_ThrowsArgumentNullExcpetion_When_MachineCodeList_Null()
        {
            //Arrange
            var machineCriterias = new MachineCriterias();

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _machineController.ReadMachineClausesListAsync(machineCriterias));

            //Assert
            Assert.NotNull(exception);
            _machineService.Verify(x => x.ReadMachineClausesListAsync(It.IsAny<string[]>()), Times.Never);
        }

        [Fact]
        public async Task Should_ReadMachineClausesListAsync_ThrowsExcpetion_When_DuplicatedCodesSent()
        {
            //Arrange
            var machineCriterias = new MachineCriterias
            {
                MachineCodeList = new Collection<string>
               {
                   "1","1"
               }
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _machineController.ReadMachineClausesListAsync(machineCriterias));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE("1"), exception.Message);
            _machineService.Verify(x => x.ReadMachineClausesListAsync(It.IsAny<string[]>()), Times.Never);
        }
    }
}
