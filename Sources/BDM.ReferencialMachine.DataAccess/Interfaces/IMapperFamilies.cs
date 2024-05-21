using System.Collections.Generic;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Interfaces
{
    public interface IMapperFamilies
    {
        ICollection<Family> Map(IEnumerable<T_FAMILY> tFamilies);
        ICollection<T_FAMILY> Map(IEnumerable<Family> families);
        ICollection<T_FAMILY> Map(Family family);
    }
}
