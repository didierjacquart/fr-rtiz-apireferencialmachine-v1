using System.Collections.Generic;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.Core.Interfaces
{
    public interface IClauseByMachineRepository
    {
        Task<ICollection<EditionClausesByMachine>> ReadMachineClausesListAsync(IEnumerable<string> machineCodes);
    }
}
