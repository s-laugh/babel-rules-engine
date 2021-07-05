using System;
using System.Collections.Generic;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public class FullRoe : BaseRoe {
        public List<FullPayPeriod> PayPeriods { get; set; }
        
        public FullRoe(SimpleRoe roe, List<FullPayPeriod> payPeriods) {
            PayPeriodType = roe.PayPeriodType;
            LastDayForWhichPaid = roe.LastDayForWhichPaid;
            FinalPayPeriodDay = roe.FinalPayPeriodDay;
            FirstDayForWhichPaid  = roe.FirstDayForWhichPaid;
            PayPeriods = new List<FullPayPeriod>(payPeriods);
        }
    }
}