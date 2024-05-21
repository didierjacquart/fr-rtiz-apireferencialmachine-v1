using System.Collections.Generic;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Interfaces
{
    public interface IMachineService
    {
        Task<ICollection<MachineSpecification>> ReadAllMachinesAsync(string product);
        Task<MachineSpecification> ReadMachineAsync(string machineCode);
        Task CreateMachineAsync(MachineSpecification machineSpecification);
        Task UpdateMachineAsync(string machineCode, MachineSpecification machineSpecification);
        Task<ICollection<MachineSpecification>> ReadMachineListAsync(IEnumerable<string> machineCodes);
        Task<ICollection<EditionClausesByMachine>> ReadMachineClausesListAsync(IEnumerable<string> machineCodes);
    }
}
