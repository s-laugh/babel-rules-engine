using System; 
using System.Collections.Generic;
using System.Linq;

namespace esdc_rules_api.AverageIncome
{
    public class AverageIncomeCalculator : ICalculateAverageIncome
    {
        private readonly long DAY = 1000 * 60 * 60 * 24;

        public decimal Calculate(AverageIncomeRequest request) {
            
            var fullRoe = CreateFullRoe(request.Roe);

            var mainStartDate = GetMainStartDate(fullRoe, request.ApplicationDate);

            var incomeList = GetIncomeList(mainStartDate, request.ApplicationDate, fullRoe);

            incomeList.Sort();

            var result = incomeList.Take(request.NumBestWeeks).Average();

            return result;
        }

        private FullRoe CreateFullRoe(SimpleRoe roe) {
            var payPeriods = new List<FullPayPeriod>();
            var endDate = roe.FinalPayPeriodDay;

            foreach (var pp in roe.PayPeriods) {
                var startDate = GetStartDateFromEndDate(endDate, roe.PayPeriodType);

                var d1 = new DateTime(Math.Min(endDate.Ticks, roe.LastDayForWhichPaid.Ticks));
                var d2 = new DateTime(Math.Max(startDate.Ticks, roe.FirstDayForWhichPaid.Ticks));
                var timespan1 = d2 - d1;
                var numDays = Math.Abs(timespan1.Days) + 1;

                decimal dailyAmount = pp.Amount / numDays;

                payPeriods.Add(new FullPayPeriod(pp.PayPeriodNumber, dailyAmount) {
                    StartDate = startDate,
                    EndDate = endDate,
                });
                endDate = new DateTime(startDate.Ticks - DAY);
            }

            return new FullRoe(roe, payPeriods);
        }

        private DateTime GetStartDateFromEndDate(DateTime endDate, string ppType) {
            DateTime startDate = new DateTime();
            // Monthly: Just take first day of the month
            if (ppType == "monthly") {
                int monthNum = endDate.Month;
                int year = endDate.Year;
                startDate = new DateTime(year, monthNum, 1, 0,0,0);
            }

            // If 15, then take 1st; else take 16
            if (ppType == "semi-monthly") {
                int monthNum = endDate.Month;
                int year = endDate.Year;
                int dayNum = endDate.Day + 1;

                // If end date is the 15th, then set it as the first day of the month
                if (dayNum == 15) {
                    startDate = new DateTime(year, monthNum, 1, 0, 0 ,0);
                }
                // If end date is NOT the  15th (then it is the 1st), then set it as the 15th day of previous month
                else {
                    if (monthNum == 0) {
                        year -=1;
                        monthNum = 12;
                    }
                    // get first day of month, then subtract one day
                    startDate = new DateTime(year, monthNum, 16, 0, 0, 0);
                }
            }

            if (ppType == "bi-weekly") {
                // 14 days
                startDate = new DateTime(endDate.Ticks - (13*DAY));
            }

            if (ppType == "weekly") {
                // 7 days
                startDate = new DateTime(endDate.Ticks - (6*DAY));
            }

            return startDate;
        }

        private DateTime GetMainStartDate(FullRoe roe, DateTime applicationDate) {
            // Take 52 weeks before application date
            var oneYearAgo = applicationDate.AddDays(- 7 * 52);

            // Compare with first day worked from RoE
            var firstDay = roe.FirstDayForWhichPaid;
            var startDateMax = Math.Max(oneYearAgo.Ticks, firstDay.Ticks);
            var startDate = new DateTime(startDateMax);

            // Find the previous sunday
            var dayOfWeek = ((int)startDate.DayOfWeek + 1) % 7;
            startDate.AddDays(-dayOfWeek);

            return startDate;
        }

        private List<decimal> GetIncomeList(DateTime startDate, DateTime endDate, FullRoe roe) {
            var incomeList = new List<decimal>();

            var startOfWeek = new DateTime(startDate.Ticks);
            var endOfWeek = startOfWeek.AddDays(6);

            while (startOfWeek < endDate) {
                decimal amount = 0;

                foreach (var pp in roe.PayPeriods) {
                    var xSpan = Math.Max(pp.StartDate.Ticks, roe.FirstDayForWhichPaid.Ticks) - startOfWeek.Ticks;
                    //var xSpanDays = xSpan / DAY;
                    var xSpanDays = (new TimeSpan(xSpan)).Days;
                    var x = Math.Min(7, Math.Max(0, xSpanDays));
                    
                    var ySpan = endOfWeek.Ticks - Math.Min(pp.EndDate.Ticks,  roe.LastDayForWhichPaid.Ticks);
                    //var ySpanDays = ySpan / DAY;
                    var ySpanDays = (new TimeSpan(ySpan)).Days;
                    var y = Math.Min(7, Math.Max(0, ySpanDays));

                    var numDays = (7 - x - y);
                    amount += pp.Amount * numDays;
                }

                incomeList.Add(amount);
                
                startOfWeek = endOfWeek.AddDays(1);
                endOfWeek = startOfWeek.AddDays(6);
            }

            return incomeList;
        }
    }
}