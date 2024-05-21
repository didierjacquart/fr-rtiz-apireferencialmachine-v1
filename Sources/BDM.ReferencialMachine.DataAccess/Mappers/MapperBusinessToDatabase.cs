using System;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;

namespace BDM.ReferencialMachine.DataAccess.Mappers
{
    public class MapperBusinessToDatabase : IMapperBusinessToDatabase
    {


        private readonly IMapperPricingRate _mapperPricingRate;
        private readonly IMapperFamilies _mapperFamilies;
        private readonly IMapperEditionClauses _mapperEditionClauses;

        public MapperBusinessToDatabase(IMapperPricingRate mapperPricingRate, IMapperFamilies mapperFamilies, IMapperEditionClauses mapperEditionClauses)
        {
            _mapperPricingRate = mapperPricingRate;
            _mapperFamilies = mapperFamilies;
            _mapperEditionClauses = mapperEditionClauses;
        }

        public MachineAndRelatedObjects Map(MachineSpecification machineSpecification)
        {
            if (machineSpecification == null)
            {
                throw new ArgumentNullException(nameof(machineSpecification));
            }

            var  tMachineSpecification = new T_MACHINE_SPECIFICATION
            {
                LABEL = machineSpecification.Label,
                NAME = machineSpecification.Name,
                CODE = machineSpecification.Code,
                DESCRIPTION = machineSpecification.Description,
                KEYWORDS = string.Join('|', machineSpecification.Keywords),
                IS_DELEGATED = machineSpecification.IsDelegated,
                IS_UNREFERENCED = machineSpecification.IsUnreferenced,
                IS_EXCLUDED = machineSpecification.IsExcluded,
                IS_OUT_OF_MACHINE_INSURANCE = machineSpecification.IsOutOfMachineInsurance,
                IS_TRANSPORTABLE = machineSpecification.IsTransportable,
                AGE_LIMIT_ALLOWED = machineSpecification.AgeLimitAllowed,
                ALL_PLACE_CORVERED = machineSpecification.AllPlacesCovered,
                EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE = machineSpecification.ExtendedFleetCoverageAllowedPercentage,
                MACHINE_RATE = machineSpecification.MachineRate,
                T_PRICING_RATE = _mapperPricingRate.Map(machineSpecification.PricingRates).ToList(),
                PRODUCT = machineSpecification.Product,
                SCORE = machineSpecification.Score,
                START_DATETIME_SUBSCRIPTION_PERIOD = machineSpecification.SubscriptionPeriod?.StartDateTime,
                END_DATETIME_SUBSCRIPTION_PERIOD = machineSpecification.SubscriptionPeriod?.EndDateTime,
                EDITION_CLAUSE_CODES = string.Join("|",machineSpecification.EditionClauses.Select(x => x.Code))
            };

            var tFamilies = _mapperFamilies.Map(machineSpecification.Families);
            var tEditionsClauses = _mapperEditionClauses.Map(machineSpecification.EditionClauses);

            var relatedElementMachine = new MachineAndRelatedObjects
            {
                T_MACHINE_SPECIFICATION = tMachineSpecification,
                T_EDITION_CLAUSE = tEditionsClauses,
                T_FAMILY = tFamilies
            };
            return relatedElementMachine;
        }

        
    }
}
