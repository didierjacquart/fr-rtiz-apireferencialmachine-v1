using System.Collections.Generic;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Interfaces
{
    public interface IFamilyService
    {
        Task<ICollection<Family>> ReadFamiliesAsync(); 
        Task CreateFamilyAsync(Family family);
        Task DeleteFamilyAsync(string familyCode);
        Task UpdateFamilyAsync(string familyCode, Family family);
    }
}
