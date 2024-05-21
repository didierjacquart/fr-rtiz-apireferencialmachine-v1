using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Interfaces
{
    public interface IMachineRepository
    {
        Task<MachineSpecification> ReadMachineAsync(string machineCode);
        Task<int> CreateMachineAsync(MachineSpecification machineSpecification);
        Task<Collection<MachineSpecification>> ReadAllMachinesAsync(string product);
        Task UpdateMachineAsync(string machineCode, MachineSpecification machineSpecification);
        Task <MachineSpecification[]> ReadMachineListAsync(string[] machineCodes);
    }
}
