using System;
using System.Collections.Generic;

namespace esdc_rules_api.AverageIncome
{
    public class BaseRoe
    {
        public string PayPeriodType { get; set; } // enum?
        public DateTime LastDayForWhichPaid { get; set; }
        public DateTime FinalPayPeriodDay { get; set; }
        public DateTime FirstDayForWhichPaid { get; set; }
        
    }

    public class SimpleRoe : BaseRoe {
        public List<PayPeriod> PayPeriods { get; set; }
    }

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