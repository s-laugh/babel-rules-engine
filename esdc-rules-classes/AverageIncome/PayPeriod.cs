using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.AverageIncome
{
    public class PayPeriod {
        [Required]
        [Range(1,100)]
        public int PayPeriodNumber { get; set; }
        [Required]
        [Range(0,Int32.MaxValue)]
        public decimal Amount { get; set; }
        public PayPeriod(int ppNumber, decimal amount) {
            PayPeriodNumber = ppNumber;
            Amount = amount;
        }
    }
}