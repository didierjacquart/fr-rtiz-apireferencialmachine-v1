using System.Collections.Generic;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyService(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task<ICollection<Family>> ReadFamiliesAsync()
        {
            return await _familyRepository.ReadFamiliesAsync();
        }

        public async Task CreateFamilyAsync(Family family)
        {
            if(string.IsNullOrEmpty(family?.Code ) || string.IsNullOrEmpty(family.Name))
            {
                throw new BadRequestException(ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_CODE,
                    ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_MESSAGE);
            }
            await _familyRepository.CreateFamilyAsync(family);
        }

        public async Task DeleteFamilyAsync(string familyCode)
        {
            await _familyRepository.DeleteFamilyAsync(familyCode);
        }

        public async Task UpdateFamilyAsync(string familyCode, Family family) 
        {
            if (string.IsNullOrEmpty(family?.Code) || string.IsNullOrEmpty(family.Name))
            {
                throw new BadRequestException(ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_CODE,
                    ExceptionConstants.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_MESSAGE);
            }
            await _familyRepository.UpdateFamilyAsync(familyCode, family);
        }
        
    }
}
