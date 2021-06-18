using System;

namespace esdc_rules_api.AverageIncome
{
    public class PayPeriod {
        public int PayPeriodNumber { get; set; }
        public decimal Amount { get; set; }
        public PayPeriod(int ppNumber, decimal amount) {
            PayPeriodNumber = ppNumber;
            Amount = amount;
        }
    }

    public class FullPayPeriod : PayPeriod {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public FullPayPeriod(int ppNumber, decimal amount) : base(ppNumber, amount) {
            
        }
    }
}