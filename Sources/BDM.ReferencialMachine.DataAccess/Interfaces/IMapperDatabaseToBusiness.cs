using System.Collections.Generic;
using System.Collections.ObjectModel;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Interfaces
{
    public interface IMapperDatabaseToBusiness
    {
        Collection<MachineSpecification> Map(IEnumerable<T_MACHINE_SPECIFICATION> dbMachines, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions);
        MachineSpecification Map(T_MACHINE_SPECIFICATION dbMachine, IEnumerable<T_EDITION_CLAUSE> dbClauses, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions);
        MachineSpecification Map(T_MACHINE_SPECIFICATION dbMachine, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions);
    }
}
