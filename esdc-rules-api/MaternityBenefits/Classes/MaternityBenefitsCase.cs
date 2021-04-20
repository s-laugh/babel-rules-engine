using System;
using System.Collections.Generic;

using esdc_rules_api.Lib;

namespace esdc_rules_api.MaternityBenefits.Classes
{
    public class MaternityBenefitsCase : IRule
    {
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }
        public Dictionary<Guid, int> BestWeeksDict { get; set; } //...
    }
}