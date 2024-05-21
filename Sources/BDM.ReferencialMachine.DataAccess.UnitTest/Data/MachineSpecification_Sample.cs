using System;
using BDM.ReferencialMachine.Core.Model;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Data
{
    public static class MachineSpecification_Sample
    {
        public static MachineSpecification GetMachineSpecification()
        {
            var machineSpecification = new MachineSpecification
            {
                Name = "Name",
                Label = "LABEL",
                Code = "CODE",
                Description = "DESCRIPTION",
                Keywords = new []{ "keyword1", "keyword2"},
                IsDelegated = true,
                IsUnreferenced = false,
                IsExcluded = false,
                IsOutOfMachineInsurance = false,
                IsTransportable = true,
                Families = MachineFamilies_Sample.GetMachineFamilies(),
                AgeLimitAllowed = 12,
                AllPlacesCovered = "POINTLESS",
                ExtendedFleetCoverageAllowedPercentage = 2,
                MachineRate = 3,
                PricingRates = PricingRates_Sample.GetPricingRates(),
                Product = "INVENTAIRE",
                Score = "1",
                SubscriptionPeriod = new TimePeriod
                {
                    StartDateTime = DateTime.Today,
                    EndDateTime = null
                },
                EditionClauses = EditionClauses_Sample.GetEditionClauses()
            };
            return machineSpecification;
        }
    }
}
