using System;
using System.Collections.Generic;

namespace esdc_rules_api.AverageIncome
{
    public class WeeklyIncomeGetter : IGetIncomeForOneWeek
    {
        private readonly IGetIncomeFromOneRoe _incomeRoeGetter;

        public WeeklyIncomeGetter(IGetIncomeFromOneRoe incomeRoeGetter) {
            _incomeRoeGetter = incomeRoeGetter;
        }

        public decimal Get(FullRoe roe, DateTime startOfWeek) {
            decimal amount = 0;

            foreach (var pp in roe.PayPeriods) {
                amount += _incomeRoeGetter.Get(pp, startOfWeek, roe.FirstDayForWhichPaid, roe.LastDayForWhichPaid);
            }
            return amount;
        }
    }
}