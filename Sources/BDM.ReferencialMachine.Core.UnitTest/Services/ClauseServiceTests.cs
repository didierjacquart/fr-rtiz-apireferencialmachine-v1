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
    public class ClauseServiceTests
    {
        private readonly Mock<IClauseRepository> _clauseRepository;
        private readonly ClauseService _clauseService;

        public ClauseServiceTests()
        {
            _clauseRepository = new Mock<IClauseRepository>();
            _clauseService = new ClauseService(_clauseRepository.Object);
        }

        [Fact]
        public async Task Should_ReadClausesAsync_Returns_ListEditionCLauses()
        {
            var expectedLstClauses = new Collection<EditionClause>();
            _clauseRepository.Setup(x => x.ReadClausesAsync()).ReturnsAsync(expectedLstClauses);
            
            var lstEditionClause = await _clauseService.ReadClausesAsync();
            
            Assert.IsType<Collection<EditionClause>>(lstEditionClause);
            _clauseRepository.Verify(x => x.ReadClausesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_CreateClauseAsync_When_Nominal_Call()
        {
            _clauseRepository.Setup(x => x.CreateClauseAsync(It.IsAny<EditionClause>()));
            
            await _clauseService.CreateClauseAsync(new EditionClause());

            _clauseRepository.Verify(x => x.CreateClauseAsync(It.IsAny<EditionClause>()), Times.Once);
        }

        [Fact]
        public async Task Should_UpdateClauseAsync_When_Nominal_Call()
        {
            _clauseRepository
                .Setup(x => x.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>()));

            await _clauseService.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>());
            
            _clauseRepository.Verify(x => x.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>()), Times.Once());
        }

        [Fact]
        public async Task Should_UpdateClauseAsync_Throws_When_ClauseCodeAreDifferent()
        {
            _clauseRepository
                .Setup(x => x.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>()));

            var clauseCode = "code";
            var editionClause = new EditionClause {Code = "AnotherCode"};

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _clauseService.UpdateClauseAsync(clauseCode, editionClause));

            Assert.Equal(ExceptionConstants.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_MESSAGE, exception.Message);

            _clauseRepository.Verify(x => x.UpdateClauseAsync(It.IsAny<string>(), It.IsAny<EditionClause>()), Times.Never());
        }

        [Fact]
        public async Task Should_DeleteClauseAsync_When_Nominal_Call()
        {
            _clauseRepository.Setup(x => x.DeleteClauseAsync(It.IsAny<string>()));
            
            await _clauseService.DeleteClauseAsync(It.IsAny<string>());

            _clauseRepository.Verify(x => x.DeleteClauseAsync(It.IsAny<string>()), Times.Once);

        }
    }
}
