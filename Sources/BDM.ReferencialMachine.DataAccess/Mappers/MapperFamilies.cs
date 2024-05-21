using System.Collections.Generic;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Mappers
{
    public class MapperFamilies : IMapperFamilies
    {
        private const string ROOT_CODE = "ROOT";

        public ICollection<Family> Map(IEnumerable<T_FAMILY> tFamilies)
        {
            if (tFamilies == null || !tFamilies.Any())
            {
                return null;
            }

            var codeToFamiliesDictionary = tFamilies.GroupBy(family => (family.CODE, family.NAME))
                .ToDictionary(group => group.Key, group => group.ToArray());

            var families = new List<Family>();

            foreach (var (key, dbFamilies) in codeToFamiliesDictionary)
            {
                var family = new Family
                {
                    Code = key.CODE,
                    Name = key.NAME,
                    SubFamilies = dbFamilies.Select(dbSubFamily =>
                            new SubFamily
                            {
                                Code = dbSubFamily.SUB_CODE,
                                Name = dbSubFamily.SUB_NAME
                            }).ToArray()
                };
                
                families.Add(family);
            }
            return families.ToArray();
        }

        public ICollection<T_FAMILY> Map(IEnumerable<Family> families)
        {
            if (families == null || !families.Any())
            {
                return null;
            }

            var dbFamilies = new List<T_FAMILY>();
            foreach (var family in families)
            {
                dbFamilies.AddRange(Map(family));
            }
            return dbFamilies.ToArray();
        }

        public ICollection<T_FAMILY> Map(Family family)
        {
            var dbFamilies = new List<T_FAMILY>();
            if (family.SubFamilies == null || !family.SubFamilies.Any())
            {
                var dbFamily = new T_FAMILY
                {
                    CODE = family.Code,
                    NAME = family.Name,
                    SUB_CODE = ROOT_CODE
                };
                dbFamilies.Add(dbFamily);
            }
            else
            {
                var dbSubFamilies = family.SubFamilies.Select(x =>
                new T_FAMILY
                {
                    CODE = family.Code,
                    NAME = family.Name,
                    SUB_CODE = x.Code,
                    SUB_NAME = x.Name
                });
                dbFamilies.AddRange(dbSubFamilies);
            }
            return dbFamilies;
        }
    }
}
