using System.Collections.Generic;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Models;
using Bogus;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{

    public static class MachineBuilder
    {
        public static Faker<MachineSpecification> FakeMachine(string machineCode)
        {
            var timePeriodBuilder = new Faker<TimePeriod>()
                .RuleFor(p => p.StartDateTime, f => f.Date.Past())
                .RuleFor(p => p.EndDateTime, f => f.Date.Future());

            var subFamilyBuilder = new Faker<SubFamily>()
                .RuleFor(x => x.Code, f => f.Random.Word())
                .RuleFor(x => x.Name, f => f.Lorem.Word());

            var familyBuilder = new Faker<Family>()
                .RuleFor(x => x.Code, f => f.Random.Word())
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.SubFamilies, subFamilyBuilder.Generate(3));

            var pricingRateBuilder = new Faker<PricingRate>()
                .RuleFor(p => p.Code, f => f.Random.Word())
                .RuleFor(p => p.Rate, f => f.Random.Decimal());


            var machineSpecificationBuilder = new Faker<MachineSpecification>()
                .RuleFor(p => p.Label, f => f.Lorem.Word())
                .RuleFor(p => p.Name, f => f.Lorem.Word())
                .RuleFor(p => p.Code, machineCode)
                .RuleFor(p => p.AllPlacesCovered, f => f.Lorem.Word())
                .RuleFor(p => p.Description, f => f.Lorem.Word())
                .RuleFor(p => p.Product, f => f.Lorem.Word())
                .RuleFor(p => p.Score, f => f.Lorem.Word())
                .RuleFor(p => p.AgeLimitAllowed, f => f.Random.Number())
                .RuleFor(p => p.ExtendedFleetCoverageAllowedPercentage, f => f.Random.Number())
                .RuleFor(p => p.IsDelegated, f => f.Random.Bool())
                .RuleFor(p => p.IsExcluded, f => f.Random.Bool())
                .RuleFor(p => p.IsOutOfMachineInsurance, f => f.Random.Bool())
                .RuleFor(p => p.IsUnreferenced, f => f.Random.Bool())
                .RuleFor(p => p.IsTransportable, f => f.Random.Bool())
                .RuleFor(p => p.Keywords, f => f.Random.WordsArray(1, 4))
                .RuleFor(p => p.MachineRate, f => f.Random.Number())
                .RuleFor(p => p.SubscriptionPeriod, timePeriodBuilder.Generate())
                .RuleFor(p => p.PricingRates, pricingRateBuilder.Generate(4))
                .RuleFor(p => p.Families, familyBuilder.Generate(3));

            return machineSpecificationBuilder;
        }

        public static Faker<T_EDITION_CLAUSE> FakeDbEditionCLause()
        {
            var editionClauseBuilder = new Faker<T_EDITION_CLAUSE>()
                .RuleFor(p => p.CODE, f => f.Random.Word())
                .RuleFor(p => p.LABEL, f => f.Lorem.Word())
                .RuleFor(p => p.TYPE, f => f.Lorem.Word())
                .RuleFor(p => p.DESCRIPTION, f => f.Lorem.Word());

            return editionClauseBuilder;
        }

        public static Faker<T_MACHINE_SPECIFICATION> FakeDbMachine(IEnumerable<T_EDITION_CLAUSE> lstEditionClauses)
        {
            var familyBuilder = new Faker<T_FAMILY>()
                .RuleFor(p => p.CODE, f => f.Random.Word())
                .RuleFor(p => p.NAME, f => f.Lorem.Word())
                .RuleFor(p => p.SUB_CODE, f => f.Lorem.Word())
                .RuleFor(p => p.SUB_NAME, f => f.Lorem.Word());

            var pricingRateBuilder = new Faker<T_PRICING_RATE>()
                .RuleFor(p => p.CODE, f => f.PickRandom("INC", "BDI", "ADE", "GTL", "VOL"))
                .RuleFor(p => p.RATE, f => f.Random.Decimal());


            var machineSpecificationBuilder = new Faker<T_MACHINE_SPECIFICATION>()
                .RuleFor(p => p.CODE, f => f.Random.Word())
                .RuleFor(p => p.NAME, f => f.Lorem.Word())
                .RuleFor(p => p.LABEL, f => f.Lorem.Word())
                .RuleFor(p => p.DESCRIPTION, f => f.Lorem.Word())
                .RuleFor(p => p.PRODUCT, f => f.Lorem.Word())
                .RuleFor(p => p.SCORE, f => f.Lorem.Word())
                .RuleFor(p => p.ALL_PLACE_CORVERED, f => f.Lorem.Word())
                .RuleFor(p => p.KEYWORDS, f => f.Lorem.Word())
                .RuleFor(p => p.START_DATETIME_SUBSCRIPTION_PERIOD, f => f.Date.Past())
                .RuleFor(p => p.END_DATETIME_SUBSCRIPTION_PERIOD, f => null)
                .RuleFor(p => p.MACHINE_RATE, f => f.Random.Number())
                .RuleFor(p => p.AGE_LIMIT_ALLOWED, f => f.Random.Number())
                .RuleFor(p => p.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE, f => f.Random.Number())
                .RuleFor(p => p.IS_DELEGATED, f => f.Random.Bool())
                .RuleFor(p => p.IS_EXCLUDED, f => f.Random.Bool())
                .RuleFor(p => p.IS_UNREFERENCED, f => f.Random.Bool())
                .RuleFor(p => p.IS_TRANSPORTABLE, f => f.Random.Bool())
                .RuleFor(p => p.IS_OUT_OF_MACHINE_INSURANCE, f => f.Random.Bool())
                .RuleFor(p => p.IS_FIRE_COVERAGE_MANDATORY, f => f.Random.Bool())
                .RuleFor(p => p.DEDUCTIBLE_MIN_INCLUDE, f => f.Random.Bool())
                .RuleFor(p => p.DEDUCTIBLE_MAX_INCLUDE, f => f.Random.Bool())
                .RuleFor(p => p.DEDUCTIBLE_MIN_VALUE, f => f.Random.Number())
                .RuleFor(p => p.DEDUCTIBLE_MAX_VALUE, f => f.Random.Number())
                .RuleFor(p => p.DEDUCTIBLE_UNIT, f => f.Lorem.Word())
                .RuleFor(p => p.FINANCIAL_VALUATION_DELEGATED_MAX, f => f.Random.Number())
                .RuleFor(p => p.T_PRICING_RATE, pricingRateBuilder.Generate(5))
                .RuleFor(p => p.T_FAMILY, familyBuilder.Generate(1))
                .RuleFor(p => p.EDITION_CLAUSE_CODES,
                    lstEditionClauses != null ? string.Join("|", lstEditionClauses.Select(x => x.CODE)) : null);


            return machineSpecificationBuilder;
        }


    }
}


