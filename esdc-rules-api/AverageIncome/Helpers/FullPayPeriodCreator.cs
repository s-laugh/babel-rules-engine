using System;

using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public class FullPayPeriodCreator : ICreateFullPayPeriods
    {
        public FullPayPeriod Create(PayPeriod payPeriod, DateTime startDate, DateTime endDate, DateTime minDate, DateTime maxDate) {
            var d1 = new DateTime(Math.Min(endDate.Ticks, maxDate.Ticks));
            var d2 = new DateTime(Math.Max(startDate.Ticks, minDate.Ticks));
            var timespan1 = d2 - d1;
            var numDays = Math.Abs(timespan1.Days) + 1;

            decimal dailyAmount = payPeriod.Amount / numDays;

            return new FullPayPeriod(payPeriod.PayPeriodNumber, dailyAmount) {
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}