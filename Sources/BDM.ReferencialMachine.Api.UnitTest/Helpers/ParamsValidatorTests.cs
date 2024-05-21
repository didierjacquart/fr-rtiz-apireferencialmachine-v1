using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Api.Helpers;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using Xunit;

namespace BDM.ReferencialMachine.Api.UnitTest.Helpers
{
    public class ParamsValidatorTests
    {
        [Fact]
        public async Task Should_CheckForDuplicateCodes_Throws_When_DuplcatedCodes()
        {
            var machineCriterias = new MachineCriterias { MachineCodeList = new Collection<string> { "1", "1" } };
            var validator = new ParamsValidator(machineCriterias, 2);
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await validator.ValidateMachineCriteriasAsync(machineCriterias));

            Assert.Equal(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE("1"), exception.Message);

        }
        [Fact]
        public async Task Should_CheckForCodesNumber_Throws_When_NbCodesExceedLimitDefined()
        {
            const int limit = 2;
            var machineCriterias = new MachineCriterias { MachineCodeList = new Collection<string> { "1", "1", "1" } };
            var validator = new ParamsValidator(machineCriterias, limit);
            var exception = await Assert.ThrowsAsync<RequestEntityTooLargeException>(async () => await validator.ValidateMachineCriteriasAsync(machineCriterias));

            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE, exception.Code);
            Assert.Equal(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(limit), exception.Message);
        }
    }
}
