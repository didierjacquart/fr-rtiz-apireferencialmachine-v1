using System.Linq;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Api.Constants;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using FluentValidation;

namespace BDM.ReferencialMachine.Api.Helpers
{
    public class ParamsValidator : AbstractValidator<MachineCriterias>
    {
        public ParamsValidator(MachineCriterias machineCriterias, int? nbCodesLimit)
        {
            var maxCodeInRequestAllowed = nbCodesLimit ?? DefaultConstants.DEFAULT_MAX_REQUESTED_CODES;
            RuleFor(criterias => criterias.MachineCodeList!.Count).LessThanOrEqualTo(maxCodeInRequestAllowed)
                .WithErrorCode(ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE)
                .WithMessage(
                    ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(maxCodeInRequestAllowed));

            var lstDuplicateCodes = machineCriterias!.MachineCodeList!.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            RuleFor(x => lstDuplicateCodes).Empty()
                .WithErrorCode(ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_CODE)
                .WithMessage(
                    ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE(string.Join(", ", lstDuplicateCodes)));
        }

        public async Task ValidateMachineCriteriasAsync(MachineCriterias machineCriterias)
        {
            var validationResult = await ValidateAsync(machineCriterias);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (error.ErrorCode == ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_CODE)
                    {
                        throw new BadRequestException(error.ErrorCode, error.ErrorMessage);
                    }

                    if (error.ErrorCode == ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE)
                    {
                        throw new RequestEntityTooLargeException(error.ErrorCode, error.ErrorMessage);
                    }
                }
            }
        }
        
    }
}
