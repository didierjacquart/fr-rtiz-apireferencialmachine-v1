using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Interfaces
{
    public interface IMapperRiskPrecision
    {
        ICollection<RiskPrecision> Map(IEnumerable<T_RISK_PRECISION> dbRiskPrecisions);
    }
}
