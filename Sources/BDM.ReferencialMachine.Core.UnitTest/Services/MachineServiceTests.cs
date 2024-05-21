using System;
using System.Collections.Generic;
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
    public class MachineServiceTests
    {
        private readonly Mock<IMachineRepository> _machineRepository;
        private readonly IMachineService _machineService;
        private readonly Mock<IClauseByMachineRepository> _clauseByMachineRepository;

        public MachineServiceTests()
        {
            _machineRepository = new Mock<IMachineRepository>();
            _clauseByMachineRepository = new Mock<IClauseByMachineRepository>();
            _machineService = new MachineService(_machineRepository.Object, _clauseByMachineRepository.Object);
        }

        [Fact]
        public async Task Should_ReadMachineAsync_Returns_MachineSpecification_When_Nominal_Call()
        {
            _machineRepository.Setup(x => x.ReadMachineAsync(It.IsAny<string>())).ReturnsAsync(new MachineSpecification());
            
            var response = await _machineService.ReadMachineAsync(It.IsAny<string>());
            
            Assert.IsType<MachineSpecification>(response);
            _machineRepository.Verify(x => x.ReadMachineAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task Should_ReadMachineAsync_Returns_Exception_When_No_Machine_Found()
        {
            var machineCode = "65021";
            _machineRepository.Setup(x => x.ReadMachineAsync(machineCode))
                .Throws(new NotFoundException(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, 
                    string.Format(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, machineCode)));
            
            var exception =
                await Assert.ThrowsAsync<NotFoundException>(() => _machineService.ReadMachineAsync(machineCode));
            
            Assert.Equal(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(string.Format(ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE, machineCode), exception.Message);
            _machineRepository.Verify(x => x.ReadMachineAsync(It.IsAny<string>()), Times.Once());
        }


        [Fact]
        public async Task Should_CreateMachineAsync_NotThrow_When_Nominal_Call()
        {
            _machineRepository.Setup(x => x.CreateMachineAsync(It.IsAny<MachineSpecification>())).ReturnsAsync(1);
            
            await _machineService.CreateMachineAsync(new MachineSpecification());

            _machineRepository.Verify(x => x.CreateMachineAsync(It.IsAny<MachineSpecification>()), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateMachine_Returns_When_Nominal_Call()
        {
            await _machineService.UpdateMachineAsync(It.IsAny<string>(), It.IsAny<MachineSpecification>());

            _machineRepository.Verify(x => x.UpdateMachineAsync(It.IsAny<string>(), It.IsAny<MachineSpecification>()), Times.Once());
        }

        [Fact]
        public async Task Should_ReadAllMachinesAsync_Returns_CollectionMachineSpecification_When_Nominal_Call()
        {
            _machineRepository.Setup(x => x.ReadAllMachinesAsync(null)).ReturnsAsync(new Collection<MachineSpecification>());

            var response = await _machineService.ReadAllMachinesAsync(null);

            Assert.IsType<Collection<MachineSpecification>>(response);
            _machineRepository.Verify(x => x.ReadAllMachinesAsync(null), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateMachine_Throws_When_CodesAreDifferent()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _machineService.UpdateMachineAsync("Code", new MachineSpecification { Code = "Another code"}));

            Assert.Equal(ExceptionConstants.MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_MESSAGE, exception.Message);

            _machineRepository.Verify(x => x.UpdateMachineAsync(It.IsAny<string>(), It.IsAny<MachineSpecification>()), Times.Never());
        }

        [Fact]
        public async Task Should_ReadAMachinesListAsync_Returns_CollectionMachineSpecification_When_Nominal_Call()
        {
            _machineRepository.Setup(x => x.ReadMachineListAsync(new[] {"1", "2"}))
                .ReturnsAsync(new[]
                {
                    new MachineSpecification(),
                    new MachineSpecification()
                });

            var response = await _machineService.ReadMachineListAsync(new[] { "1", "2" });

            Assert.IsType<MachineSpecification[]>(response);
            Assert.Equal(2, response.Count);
            _machineRepository.Verify(x => x.ReadMachineListAsync(new[] { "1", "2" }), Times.Once);
        }

        [Fact]
        public async Task Should_ReadAMachinesListAsync_Returns_EmptyList_When_NullInput()
        {
            var result = await _machineService.ReadMachineListAsync(null);
            
            Assert.Empty(result);
        }

        [Fact]
        public async Task Should_Update_Machine_Throws_When_Duplicated_Clause_Codes()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _machineService.UpdateMachineAsync("code", 
                new MachineSpecification
                {
                    Code = "code",
                    EditionClauses = new List<EditionClause>
                    {
                        new EditionClause{ Code = "1"},
                        new EditionClause{ Code = "1"},
                        new EditionClause{ Code = "2"},
                        new EditionClause{ Code = "2"},
                        new EditionClause{ Code = "3"}
                    }
                }));

            Assert.Equal(ExceptionConstants.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_MESSAGE(new List<string>{"1","2"}), exception.Message);
        }

        [Fact]
        public async Task Should_Create_Machine_Throws_When_Duplicated_Clause_Codes()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _machineService.CreateMachineAsync(
                new MachineSpecification
                {
                    Code = "code",
                    EditionClauses = new List<EditionClause>
                    {
                        new EditionClause{ Code = "1"},
                        new EditionClause{ Code = "1"},
                        new EditionClause{ Code = "2"},
                        new EditionClause{ Code = "2"},
                        new EditionClause{ Code = "3"}
                    }
                }));

            Assert.Equal(ExceptionConstants.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_MESSAGE(new List<string> { "1", "2" }), exception.Message);
        }

        [Fact]
        public async Task Should_ReadMachineClausesListAsync_Returns_ArrayEditionClausesByMachine_When_Nominal_Call()
        {
            _clauseByMachineRepository.Setup(x => x.ReadMachineClausesListAsync(new[] { "1", "2" }))
                .ReturnsAsync(new[]
                {
                    new EditionClausesByMachine(),
                    new EditionClausesByMachine()
                });

            var response = await _machineService.ReadMachineClausesListAsync(new[] { "1", "2" });

            Assert.IsType<EditionClausesByMachine[]>(response);
            Assert.Equal(2, response.Count);
            _clauseByMachineRepository.Verify(x => x.ReadMachineClausesListAsync(new[] { "1", "2" }), Times.Once);
        }
        
        [Fact]
        public async Task Should_ReadMachineClausesListAsync_Returns_EmptyList_When_NullInput()
        {
            var result = await _machineService.ReadMachineClausesListAsync(null);

            Assert.Empty(result);
        }

    }
}
