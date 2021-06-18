using System;
using System.Collections.Generic;

namespace esdc_rules_api.BestWeeks
{
    public class BestWeeksCalculator : ICalculateBestWeeks
    {
        private int DEFAULT_BEST_WEEKS = 14;
        private readonly Dictionary<string, int> _bestWeeksLookup = new Dictionary<string, int>() {
            { "01", 14 },
            { "02", 15 },
        };
        
        public int Calculate(string postalCode) 
        {
            var economicRegion = GetEconomicRegion(postalCode);
            var bestWeeks = _bestWeeksLookup.GetValueOrDefault(economicRegion, DEFAULT_BEST_WEEKS);
            return bestWeeks;
        }

        public string GetEconomicRegion(string postalCode) {
            // TODO: Will need to add in some system to do this lookup...
            // May need hard-coding for now.
            return "01";
        }

        
    }
}