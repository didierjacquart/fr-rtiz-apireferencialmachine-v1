using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Mappers
{
    public class MapperRiskPrecision : IMapperRiskPrecision
    {
        public ICollection<RiskPrecision> Map(IEnumerable<T_RISK_PRECISION> dbRiskPrecisions)
        {
            if (dbRiskPrecisions == null || !dbRiskPrecisions.Any())
            {
                return null;
            }

            var riskPrecisions = new Collection<RiskPrecision>();
            foreach (var dbRiskPrecision in dbRiskPrecisions)
            {
                var riskPrecision = new RiskPrecision
                {
                    Code = dbRiskPrecision.CODE,
                    Detail = dbRiskPrecision.DETAIL,
                    Label = dbRiskPrecision.LABEL
                };
                riskPrecisions.Add(riskPrecision);
            }

            return riskPrecisions;
        }
    }
}
