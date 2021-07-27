using System;
using System.Collections.Generic;


namespace esdc_rules_api.AverageIncome
{
    public class IncomeListGetter : IGetIncomeList
    {

        private readonly IGetMainStartDate _mainStartDateGetter;
        private readonly IGetIncomeForOneWeek _weeklyIncomeGetter;

        public IncomeListGetter(
            IGetMainStartDate mainStartDateGetter,
            IGetIncomeForOneWeek weeklyIncomeGetter) {
            _mainStartDateGetter = mainStartDateGetter;
            _weeklyIncomeGetter = weeklyIncomeGetter;
        }
        public List<decimal> Get(DateTime applicationDate, FullRoe roe) {
            var startOfWeek = _mainStartDateGetter.Get(roe.FirstDayForWhichPaid, applicationDate);
            var incomeList = new List<decimal>();

            while (startOfWeek < applicationDate) {
                decimal amount = _weeklyIncomeGetter.Get(roe, startOfWeek);      
                incomeList.Add(amount);
                startOfWeek = startOfWeek.AddDays(7);
            }

            return incomeList;
        }
    }
}