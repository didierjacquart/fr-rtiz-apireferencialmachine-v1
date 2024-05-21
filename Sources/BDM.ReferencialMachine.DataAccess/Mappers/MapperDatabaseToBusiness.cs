using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;
using MC = BDM.ReferencialMachine.Core.Constants.MachineConstants;

namespace BDM.ReferencialMachine.DataAccess.Mappers
{
    public class MapperDatabaseToBusiness : IMapperDatabaseToBusiness
    {
        private readonly IMapperPricingRate _mapperPricingRate;
        private readonly IMapperFamilies _mapperFamilies;
        private readonly IMapperEditionClauses _mapperEditionClauses;
        private readonly IMapperRiskPrecision _mapperRiskPrecision;

        public MapperDatabaseToBusiness(
            IMapperPricingRate mapperPricingRate,
            IMapperFamilies mapperFamilies,
            IMapperEditionClauses mapperEditionClauses,
            IMapperRiskPrecision mapperRiskPrecision)
        {
            _mapperPricingRate = mapperPricingRate;
            _mapperFamilies = mapperFamilies;
            _mapperEditionClauses = mapperEditionClauses;
            _mapperRiskPrecision = mapperRiskPrecision;
        }

        public Collection<MachineSpecification> Map(IEnumerable<T_MACHINE_SPECIFICATION> dbMachines, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions)
        {
            if (dbMachines == null)
            {
                throw new ArgumentNullException(nameof(dbMachines));
            }

            var machines = new Collection<MachineSpecification>();
            foreach (var tMachineSpecification in dbMachines)
            {
                machines.Add(Map(tMachineSpecification, dbRiskPrecisions));
            }
            return machines;
        }

        public MachineSpecification Map(T_MACHINE_SPECIFICATION dbMachine, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions)
        {
            if (dbMachine == null)
            {
                throw new ArgumentNullException(nameof(dbMachine));
            }
            return Map(dbMachine, null, dbRiskPrecisions);
        }

        public MachineSpecification Map(T_MACHINE_SPECIFICATION dbMachine, IEnumerable<T_EDITION_CLAUSE> dbClauses, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions)
        {
            if (dbMachine == null)
            {
                return null;
            }

            var machineSpecification = new MachineSpecification
            {
                Label = dbMachine.LABEL,
                Name = dbMachine.NAME,
                Code = dbMachine.CODE,
                Description = dbMachine.DESCRIPTION,
                IsDelegated = dbMachine.IS_DELEGATED,
                IsUnreferenced = dbMachine.IS_UNREFERENCED,
                IsExcluded = dbMachine.IS_EXCLUDED,
                IsTransportable = dbMachine.IS_TRANSPORTABLE,
                Families = _mapperFamilies.Map(dbMachine.T_FAMILY),
                AgeLimitAllowed = dbMachine.AGE_LIMIT_ALLOWED,
                AllPlacesCovered = dbMachine.ALL_PLACE_CORVERED,
                IsOutOfMachineInsurance = dbMachine.IS_OUT_OF_MACHINE_INSURANCE,
                MachineRate = dbMachine.MACHINE_RATE,
                Product = dbMachine.PRODUCT,
                Score = dbMachine.SCORE
            };

            MapEditionClauses(machineSpecification, dbClauses);

            MapDeductible(machineSpecification, dbMachine);
            
            MapForSpecificProduct(machineSpecification, dbMachine, dbRiskPrecisions);

            MapKeywords(machineSpecification, dbMachine);

            MapEffectivePeriod(machineSpecification, dbMachine);

            MapPricingRate(machineSpecification, dbMachine);

            return machineSpecification;
        }

        private void MapPricingRate(MachineSpecification machineSpecification, T_MACHINE_SPECIFICATION dbMachine)
        {
            if (dbMachine.T_PRICING_RATE == null || !dbMachine.T_PRICING_RATE.Any())
            {
                return;
            }

            machineSpecification.PricingRates = _mapperPricingRate.Map(dbMachine.T_PRICING_RATE);
        }

        private void MapForSpecificProduct(MachineSpecification machineSpecification, T_MACHINE_SPECIFICATION dbMachine, IEnumerable<T_RISK_PRECISION> dbRiskPrecisions)
        {
            var productsToEnrich = new[] { MC.PARK, MC.RENTAL_MACHINE };
            if (productsToEnrich.All(product => machineSpecification.Product != product))
            {
                return;
            }

            if (machineSpecification.Product == MC.PARK || machineSpecification.Product == MC.RENTAL_MACHINE)
            {
                machineSpecification.RiskPrecisions = _mapperRiskPrecision.Map(dbRiskPrecisions.Where(x => x.MACHINE_CODE == dbMachine.CODE));
                machineSpecification.FinancialValuationDelegatedMax = dbMachine.FINANCIAL_VALUATION_DELEGATED_MAX;
            }

            if (machineSpecification.Product == MC.PARK)
            {
                machineSpecification.IsFireCoverageMandatory = dbMachine.IS_FIRE_COVERAGE_MANDATORY;
                machineSpecification.ExtendedFleetCoverageAllowedPercentage = dbMachine.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE;
                return;
            }
            
            // only for RENTAL_MACHINE
            machineSpecification.FinancialValuationMachinedMax = new CurrencyAmount
            {
                Amount = dbMachine.FINANCIAL_VALUATION_MACHINE_MAX,
                CurrencyCode = dbMachine.FINANCIAL_VALUATION_MACHINE_CURRENCY
            };

            machineSpecification.FinancialValuationMachineDelegatedMax = new CurrencyAmount
            {
                Amount = dbMachine.FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX,
                CurrencyCode = dbMachine.FINANCIAL_VALUATION_MACHINE_CURRENCY
            };
        }

        private static void MapEffectivePeriod(MachineSpecification machineSpecification, T_MACHINE_SPECIFICATION dbMachine)
        {
            if (dbMachine.START_DATETIME_SUBSCRIPTION_PERIOD == null)
            {
                return;
            }

            machineSpecification.SubscriptionPeriod = new TimePeriod
            {
                StartDateTime = dbMachine.START_DATETIME_SUBSCRIPTION_PERIOD,
                EndDateTime = dbMachine.END_DATETIME_SUBSCRIPTION_PERIOD
            };
        }

        private static void MapKeywords(MachineSpecification machineSpecification, T_MACHINE_SPECIFICATION dbMachine)
        {
            if (string.IsNullOrEmpty(dbMachine.KEYWORDS))
            {
                return;
            }

            machineSpecification.Keywords = dbMachine.KEYWORDS.Split(new[] { '|' });
        }

        private static void MapDeductible(MachineSpecification machineSpecification, T_MACHINE_SPECIFICATION dbMachine)
        {
            if (dbMachine.DEDUCTIBLE_MAX_VALUE == null && dbMachine.DEDUCTIBLE_MIN_VALUE == null)
            {
                return;
            }

            machineSpecification.AgreementDeductible = new Deductible
            {
                DeductibleInterval = new DeductibleInterval
                {
                    MaximumInclude = dbMachine.DEDUCTIBLE_MAX_INCLUDE,
                    MaximumValue = dbMachine.DEDUCTIBLE_MAX_VALUE,
                    MinimumInclude = dbMachine.DEDUCTIBLE_MIN_INCLUDE,
                    MinimumValue = dbMachine.DEDUCTIBLE_MIN_VALUE,
                    Unit = dbMachine.DEDUCTIBLE_UNIT
                }
            };
        }

        private void MapEditionClauses(MachineSpecification machineSpecification, IEnumerable<T_EDITION_CLAUSE> dbClauses)
        {
            var dbEditionClauses = dbClauses?.ToList();
            if (dbEditionClauses is not { Count: > 0 })
            {
                return;
            }

            machineSpecification.EditionClauses = _mapperEditionClauses.Map(dbEditionClauses);
        }
    }
}
