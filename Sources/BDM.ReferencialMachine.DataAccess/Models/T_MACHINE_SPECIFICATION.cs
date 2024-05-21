using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BDM.ReferencialMachine.DataAccess.Models
{
    public class T_MACHINE_SPECIFICATION
    {
        public T_MACHINE_SPECIFICATION()
        {
            T_FAMILY = new List<T_FAMILY>();
            T_PRICING_RATE = new List<T_PRICING_RATE>();
        }

        public Guid MACHINE_ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string LABEL { get; set; }
        public string DESCRIPTION { get; set; }
        public string KEYWORDS { get; set; }
        public string SCORE { get; set; }
        public DateTime? START_DATETIME_SUBSCRIPTION_PERIOD { get; set; }
        public DateTime? END_DATETIME_SUBSCRIPTION_PERIOD { get; set; }
        public bool? IS_DELEGATED { get; set; }
        public bool? IS_OUT_OF_MACHINE_INSURANCE { get; set; }
        public bool? IS_UNREFERENCED { get; set; }
        public bool? IS_EXCLUDED { get; set; }
        public bool? IS_TRANSPORTABLE { get; set; }
        public int? AGE_LIMIT_ALLOWED { get; set; }
        public string ALL_PLACE_CORVERED { get; set; }
        public int? EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE { get; set; }
        public string PRODUCT { get; set; }
        public decimal? MACHINE_RATE { get; set; }
        public string EDITION_CLAUSE_CODES { get; set; }
        public int? DEDUCTIBLE_MAX_VALUE { get; set; }
        public int? DEDUCTIBLE_MIN_VALUE { get; set; }
        public bool? DEDUCTIBLE_MIN_INCLUDE { get; set; }
        public bool? DEDUCTIBLE_MAX_INCLUDE { get; set; }
        public string DEDUCTIBLE_UNIT { get; set; }
        public bool? IS_FIRE_COVERAGE_MANDATORY { get; set; }
        public int? FINANCIAL_VALUATION_DELEGATED_MAX { get; set; }
        public int? FINANCIAL_VALUATION_MACHINE_MAX { get; set; }
        public int? FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX { get; set; }
        public string FINANCIAL_VALUATION_MACHINE_CURRENCY { get; set; }

        public virtual List<T_FAMILY> T_FAMILY { get; set; }
        public virtual List<T_PRICING_RATE> T_PRICING_RATE { get; set; }
    }
}
