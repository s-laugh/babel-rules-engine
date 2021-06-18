using System;
using System.Collections.Generic;

namespace esdc_rules_api.AverageIncome
{
    public class AverageIncomeRequest
    {
        public SimpleRoe Roe { get; set; }
        public DateTime ApplicationDate { get; set;}
        public int NumBestWeeks { get; set; }
    }
}