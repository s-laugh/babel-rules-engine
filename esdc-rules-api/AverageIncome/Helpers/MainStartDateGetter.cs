using System;

namespace esdc_rules_api.AverageIncome
{
    public class MainStartDateGetter : IGetMainStartDate
    {
        public DateTime Get(DateTime firstDayForWhichPaid, DateTime applicationDate) {
            // Take 52 weeks before application date
            var oneYearAgo = applicationDate.AddDays(- 7 * 52);

            // Compare with first day worked from RoE
            var firstDay = firstDayForWhichPaid;
            var startDateMax = Math.Max(oneYearAgo.Ticks, firstDay.Ticks);
            var startDate = new DateTime(startDateMax);

            // Find the previous sunday
            var dayOfWeek = ((int)startDate.DayOfWeek) % 7;

            return startDate.AddDays(-dayOfWeek);
        }

    }
}