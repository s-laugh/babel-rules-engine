using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.AverageIncome
{
    public class BaseRoe
    {
        [Required]
        public string PayPeriodType { get; set; }
        [Required]
        public DateTime LastDayForWhichPaid { get; set; }
        [Required]
        public DateTime FinalPayPeriodDay { get; set; }
        [Required]
        public DateTime FirstDayForWhichPaid { get; set; }
        
    }

    public class SimpleRoe : BaseRoe {
        public List<PayPeriod> PayPeriods { get; set; }
    }
}