using System;
using System.Collections.Generic;

using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public class FullRoeCreator : ICreateFullRoes
    {
        private readonly IGetStartDateFromEndDate _startDateGetter;
        private readonly ICreateFullPayPeriods _payPeriodCreator;
                
        public FullRoeCreator(
            IGetStartDateFromEndDate startDateGetter,
            ICreateFullPayPeriods payPeriodCreator) {
            _startDateGetter = startDateGetter;
            _payPeriodCreator = payPeriodCreator;
        }
        public FullRoe Create(SimpleRoe roe) {
            var payPeriods = new List<FullPayPeriod>();
            var endDate = roe.FinalPayPeriodDay;

            foreach (var pp in roe.PayPeriods) {
                var startDate = _startDateGetter.Get(endDate, roe.PayPeriodType);
                var nextPP = _payPeriodCreator.Create(pp, startDate, endDate, roe.FirstDayForWhichPaid, roe.LastDayForWhichPaid);
                payPeriods.Add(nextPP);
                endDate = startDate.AddDays(-1);
            }

            return new FullRoe(roe, payPeriods);
        }

    }
}