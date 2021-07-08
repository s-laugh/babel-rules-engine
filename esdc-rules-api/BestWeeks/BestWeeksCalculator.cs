using System;
using System.Collections.Generic;

namespace esdc_rules_api.BestWeeks
{
    public class BestWeeksCalculator : ICalculateBestWeeks
    {
        private readonly int DEFAULT_BEST_WEEKS = 14;
        
        public int Calculate(string postalCode) 
        {
            // Source: https://srv129.services.gc.ca/eiregions/eng/rates_cur.aspx
            // TODO: Eventually want a dynamic system to handle this
            return DEFAULT_BEST_WEEKS;
        }        
    }
}