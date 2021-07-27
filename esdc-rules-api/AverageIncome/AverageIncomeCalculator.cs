using System; 
using System.Collections.Generic;
using System.Linq;

using esdc_rules_classes.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.AverageIncome
{
    public class AverageIncomeCalculator : ICalculateAverageIncome
    {

        private readonly ICreateFullRoes _fullRoeCreator;
        private readonly IGetIncomeList _incomeListGetter;

        public AverageIncomeCalculator(
            ICreateFullRoes fullRoeCreator,
            IGetIncomeList incomeListGetter
        ) {
            _fullRoeCreator = fullRoeCreator;
            _incomeListGetter = incomeListGetter;
        }

        public decimal Calculate(AverageIncomeRequest request) {
            var fullRoe = _fullRoeCreator.Create(request.Roe);
            var incomeList = _incomeListGetter.Get(request.ApplicationDate, fullRoe);

            incomeList.Sort((a, b) => b.CompareTo(a));
            var result = incomeList.Take(request.NumBestWeeks).Average();

            return result;
        }
    }
}