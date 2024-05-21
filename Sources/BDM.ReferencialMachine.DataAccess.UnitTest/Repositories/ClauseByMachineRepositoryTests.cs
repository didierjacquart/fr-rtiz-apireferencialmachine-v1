using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
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
    public class ClauseByMachineRepositoryTests
    {
        private readonly Mock<ILogger<ClauseByMachineRepository>> _logger;
        private readonly MachineContext _context;
        private readonly ClauseByMachineRepository _repository;
        private readonly Mock<IMapperEditionClauses> _mapper;

        public ClauseByMachineRepositoryTests()
        {
            _logger = new Mock<ILogger<ClauseByMachineRepository>>();
            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _mapper = new Mock<IMapperEditionClauses>();
            _context = new MachineContext(options);
            _repository = new ClauseByMachineRepository(_context, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task Should_GetClausesByMachine_When_NominalCall()
        {
            //Arrange
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(2);
            var dbMachines = MachineBuilder.FakeDbMachine(dbClauses).Generate(1);

            _context.AddRange(dbClauses);
            _context.AddRange(dbMachines);

            await _context.SaveChangesAsync();
            var expectedMachine = dbMachines.FirstOrDefault();
            var expectedClause = dbClauses.FirstOrDefault();

            _mapper.Setup(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>())).Returns(new List<EditionClause>
            {
                new EditionClause
                {
                    Code = expectedClause.CODE,
                    Description = expectedClause.DESCRIPTION,
                    Label = expectedClause.LABEL,
                    Type = expectedClause.TYPE
                }
            });
            
            //Act
            var response = await _repository.ReadMachineClausesListAsync(new [] {expectedMachine.CODE});

            //Assert
            Assert.Single(response);
            var editionClausesByMachine = response.FirstOrDefault();
            
            Assert.Equal(expectedMachine.CODE, editionClausesByMachine.MachineCode);

            var editionClauseByClause = editionClausesByMachine.EditionClauseList.FirstOrDefault(x => x.Code == expectedClause.CODE);
            Assert.NotNull(editionClauseByClause);
            Assert.Equal(expectedClause.CODE, editionClauseByClause.Code);
            Assert.Equal(expectedClause.LABEL, editionClauseByClause.Label);
            Assert.Equal(expectedClause.DESCRIPTION, editionClauseByClause.Description);
            Assert.Equal(expectedClause.TYPE, editionClauseByClause.Type);
            
        }

        [Fact]
        public async Task Should_GetClausesByMachine_Returns_Only_Clause_For_Known_Machines()
        {
            //Arrange
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(2);
            var dbMachines = MachineBuilder.FakeDbMachine(dbClauses).Generate(1);
            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            await _context.SaveChangesAsync();

            var knownMachineCode = dbMachines.First().CODE;
            var unknownMachineCode = "2";

            //Act
            var result = await _repository.ReadMachineClausesListAsync(new[] { knownMachineCode, unknownMachineCode });

            //Assert
            Assert.Single(result);
            Assert.Equal(knownMachineCode, result.First().MachineCode);
            Assert.Null(result.FirstOrDefault(x => x.MachineCode == "2"));
        }
    }
}
