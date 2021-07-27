using System;
using System.Collections.Generic;


namespace esdc_rules_api.AverageIncome
{
    public class IncomeRoeGetter : IGetIncomeFromOneRoe
    {
        public decimal Get(FullPayPeriod payPeriod, DateTime startOfWeek, DateTime minDate, DateTime maxDate) {
                var endOfWeek = startOfWeek.AddDays(6);

                var xSpan = Math.Max(payPeriod.StartDate.Ticks, minDate.Ticks) - startOfWeek.Ticks;
                var xSpanDays = (new TimeSpan(xSpan)).Days;
                var x = Math.Min(7, Math.Max(0, xSpanDays));
                
                var ySpan = endOfWeek.Ticks - Math.Min(payPeriod.EndDate.Ticks, maxDate.Ticks);
                var ySpanDays = (new TimeSpan(ySpan)).Days;
                var y = Math.Min(7, Math.Max(0, ySpanDays));

                var numDays = (7 - x - y);
                return payPeriod.Amount * numDays;
        }
    }
}