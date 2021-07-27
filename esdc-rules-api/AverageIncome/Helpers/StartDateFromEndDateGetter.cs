using System;

using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.AverageIncome
{
    public class StartDateFromEndDateGetter : IGetStartDateFromEndDate
    {
        // TODO: Could make this polymorphic
        public DateTime Get(DateTime endDate, string ppType) { 
            if (ppType == ppTypes.MONTHLY) {
                return GetMonthly(endDate);
            }

            if (ppType == ppTypes.SEMIMONTHLY) {
                return GetSemiMonthly(endDate);
            }

            if (ppType == ppTypes.BIWEEKLY) {
                return GetBiWeekly(endDate);
            }

            if (ppType == ppTypes.WEEKLY) {
                return GetWeekly(endDate);
            }

            throw new Exception($"Invalid Week type: {ppType}");
        }

        // Monthly: Just take first day of the month
        private DateTime GetMonthly(DateTime endDate) {
            int monthNum = endDate.Month;
            int year = endDate.Year;
            return new DateTime(year, monthNum, 1, 0,0,0);
        }
        
        // SemiMonthly: If 15th, then take 1st; else take 16
        private DateTime GetSemiMonthly(DateTime endDate) {
            DateTime startDate;
            int monthNum = endDate.Month;
            int year = endDate.Year;
            int dayNum = endDate.Day;

            // TODO: Should this validate the endDate that comes in?

            // If end date is the 15th, then set it as the first day of the month
            if (dayNum == 15) {
                startDate = new DateTime(year, monthNum, 1, 0, 0 ,0);
            }
            // If end date is NOT the  15th (then it is the end of month), then set it as the 16th day 
            else {
                if (monthNum == 0) {
                    year -=1;
                    monthNum = 12;
                }
                startDate = new DateTime(year, monthNum, 16, 0, 0, 0);
            }
            return startDate;
        }

        // 2 Weeks ago
        private DateTime GetBiWeekly(DateTime endDate) {
            return ZeroDate(endDate.AddDays(-13));
        
        }

        // 1 Week ago
        private DateTime GetWeekly(DateTime endDate) {
            return ZeroDate(endDate.AddDays(-6));
        }

        private DateTime ZeroDate(DateTime theDate) {
            return theDate
                .AddHours(-theDate.Hour)
                .AddMinutes(-theDate.Minute)
                .AddSeconds(-theDate.Second)
                .AddMilliseconds(-theDate.Millisecond);
        }
    }
}