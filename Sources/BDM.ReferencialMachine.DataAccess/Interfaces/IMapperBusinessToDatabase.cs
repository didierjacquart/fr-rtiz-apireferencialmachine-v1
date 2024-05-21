using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Interfaces
{
    public interface IMapperBusinessToDatabase
    {
        MachineAndRelatedObjects Map(MachineSpecification machineSpecification);
    }
}
