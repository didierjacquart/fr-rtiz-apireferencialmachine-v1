using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ClauseRepositoryTests
    {
        private readonly Mock<ILogger<ClauseRepository>> _logger;
        private readonly Mock<IMapperEditionClauses> _mapperEditionClause;
        private readonly MachineContext _context;
        private readonly ClauseRepository _repository;

        public ClauseRepositoryTests()
        {
            _logger = new Mock<ILogger<ClauseRepository>>();
            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _mapperEditionClause = new Mock<IMapperEditionClauses>();
            _context = new MachineContext(options);
            _repository = new ClauseRepository(_context, _mapperEditionClause.Object, _logger.Object);
        }

        [Fact]
        public async Task Should_Read_Return_ListEditionClause_On_Nominal_Call()
        {
            //Arrange
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(1);
            
            _context.AddRange(dbClauses);

            var expectedClause = dbClauses.First();

            _mapperEditionClause
                .Setup(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()))
                .Returns(new Collection<EditionClause>
                {
                    new EditionClause
                    {
                        Code = expectedClause.CODE,
                        Description = expectedClause.DESCRIPTION,
                        Label = expectedClause.LABEL,
                        Type = expectedClause.TYPE
                    }
                });
            
            var lstEditionClause = await _repository.ReadClausesAsync();

            Assert.NotNull(lstEditionClause);
            Assert.IsType<Collection<EditionClause>>(lstEditionClause);
            _mapperEditionClause.Verify(x => x.Map(It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()), Times.Once);

        }

        [Fact]
        public async Task Should_Delete_Throws_When_EditionClause_Attached_Machines()
        {
            var machineCode = "65021";
            var clauseCode = "MA16";
            _context.Add(new T_EDITION_CLAUSE { LABEL = "Label1", CODE = clauseCode });
            _context.T_MACHINE_SPECIFICATION.Add(new T_MACHINE_SPECIFICATION
            {
                CODE = machineCode,
                EDITION_CLAUSE_CODES = clauseCode
            });
            await _context.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<BadRequestException>(async() => await _repository.DeleteClauseAsync(clauseCode));
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_MESSAGE(clauseCode), exception.Message);

        }

        [Fact]
        public async Task Should_Delete_On_NominalCall()
        {
            var clauseCode = "MA16";
            _context.T_EDITION_CLAUSE.Add(new T_EDITION_CLAUSE{ LABEL = "Label1", CODE = clauseCode });
            await _context.SaveChangesAsync();

            await _repository.DeleteClauseAsync(clauseCode);
            
            Assert.Empty(_context.T_EDITION_CLAUSE.ToList());
        }

        [Fact]
        public async Task Should_Delete_Throws_When_Clause_NotFound()
        {
            var machineCode = "65021";
            var clauseCode = "MA19";
            _context.T_MACHINE_SPECIFICATION.Add(new T_MACHINE_SPECIFICATION
            {
                CODE = machineCode,
                EDITION_CLAUSE_CODES = clauseCode
            });

            await _context.SaveChangesAsync();

            var repository = new ClauseRepository(_context, _mapperEditionClause.Object, _logger.Object);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => repository.DeleteClauseAsync(clauseCode));

            Assert.Equal(ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE(clauseCode), exception.Message);
        }

        [Fact]
        public async Task Should_CreateClauseAsync_On_Nominal_Call()
        {
            _context.AddRange(new List<T_EDITION_CLAUSE>
                {
                    new T_EDITION_CLAUSE{ LABEL = "Label1", CODE = "1234" }
                }
            );
            await _context.SaveChangesAsync();

            _mapperEditionClause.Setup(x => x.Map(It.IsAny<EditionClause>())).Returns(new T_EDITION_CLAUSE
            {
                CODE = "Code",
                LABEL = "Label"
            });

            await _repository.CreateClauseAsync(new EditionClause { Code = "Code", Label = "Label"});
 
            var lstClauses = _context.T_EDITION_CLAUSE.ToArray();

            Assert.Equal(2, lstClauses.Length);
        }

        [Fact]
        public async Task Should_CreateClauseAsync_Throws_When_ClauseAlreadyExist()
        {
            var clauseCode = "1234";
            _context.AddRange(new List<T_EDITION_CLAUSE>
                {
                    new T_EDITION_CLAUSE{ LABEL = "Label1", CODE = clauseCode }
                }
            );
            await _context.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _repository.CreateClauseAsync(new EditionClause { Code = clauseCode, Label = "Bob" }));

            Assert.Equal(ExceptionConstants.CLAUSE_ALREADY_EXIST_EXCCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.CLAUSE_ALREADY_EXIST_EXCCEPTION_MESSAGE(clauseCode), exception.Message);
        }

        [Theory]
        [InlineData("code", "")]
        [InlineData("code", null)]
        [InlineData("", "label")]
        [InlineData(null, "label")]
        [InlineData(null, null)]
        public async Task Should_CreateClauseAsync_Throws_When_ClauseNotWellFormed(string code, string label)
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _repository.CreateClauseAsync(new EditionClause { Code = code, Label = label }));

            Assert.Equal(ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE, exception.Message);
        }

        [Fact]
        public async Task Should_UpdateClauseAsync_On_Nominal_Call()
        {     
            const string clauseCode = "MA19";
            
            _context.T_EDITION_CLAUSE.AddRange(new List<T_EDITION_CLAUSE> { new T_EDITION_CLAUSE{ LABEL = "Label1", CODE = clauseCode } } );
            await _context.SaveChangesAsync();

            await _repository.UpdateClauseAsync(clauseCode,  new EditionClause { Code = clauseCode, Label = "Label" });

            var updatedClause = _context.T_EDITION_CLAUSE.FirstOrDefault(x => x.CODE == clauseCode);

            Assert.NotNull(updatedClause);
            Assert.Equal(clauseCode, updatedClause.CODE);
            Assert.Equal("Label", updatedClause.LABEL);
        }
        
        [Fact]
        public async Task Should_UpdateClauseAsync_When_ClauseNotFound()
        {
            var machineCode = "65021";
            var clauseCode = "MA19";
            
            _context.T_MACHINE_SPECIFICATION.Add(new T_MACHINE_SPECIFICATION
            {
                CODE = machineCode,
                EDITION_CLAUSE_CODES= clauseCode
            });
            await _context.SaveChangesAsync();
            
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _repository.UpdateClauseAsync(clauseCode, new EditionClause { Code = "Code", Label = "Label" }));

            Assert.Equal(ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE(clauseCode), exception.Message);
        }

        [Theory]
        [InlineData("code", "")]
        [InlineData("code", null)]
        [InlineData("", "label")]
        [InlineData(null, "label")]
        [InlineData(null, null)]
        public async Task Should_UpdateClauseAsync_Throws_When_ClauseNotWellFormed(string code, string label)
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _repository.UpdateClauseAsync("Bob", new EditionClause { Code = code, Label = label }));

            Assert.Equal(ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE, exception.Message);
        }
       
    }
}
