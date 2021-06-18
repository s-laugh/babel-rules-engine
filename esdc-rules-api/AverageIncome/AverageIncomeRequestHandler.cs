using System;
using System.Collections.Generic;

namespace esdc_rules_api.AverageIncome
{
    public class AverageIncomeRequestHandler : IHandleAverageIncomeRequests
    {
        private readonly ICalculateAverageIncome _calculator;

        public AverageIncomeRequestHandler(ICalculateAverageIncome calculator)
        {
            _calculator = calculator;
        }
        public AverageIncomeResponse Handle(AverageIncomeRequest request) {
            var result = _calculator.Calculate(request);

            return new AverageIncomeResponse {
                AverageIncome = result
            };
        }
    }
}