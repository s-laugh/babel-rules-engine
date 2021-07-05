using System;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public class FullPayPeriod : PayPeriod {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public FullPayPeriod(int ppNumber, decimal amount) : base(ppNumber, amount) {
            
        }
    }
}