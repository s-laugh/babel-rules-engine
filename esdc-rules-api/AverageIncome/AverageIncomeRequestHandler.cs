using System;
using System.Collections.Generic;
using esdc_rules_api.Lib;

using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public class AverageIncomeRequestHandler : IHandleRequests<AverageIncomeRequest, AverageIncomeResponse>
    {
        private readonly ICalculateAverageIncome _calculator;
        private readonly IValidateRequests<AverageIncomeRequest> _validator;

        public AverageIncomeRequestHandler(
            ICalculateAverageIncome calculator,
            IValidateRequests<AverageIncomeRequest> validator)
        {
            _calculator = calculator;
            _validator = validator;
        }
        public AverageIncomeResponse Handle(AverageIncomeRequest request) {
            _validator.Validate(request);
            var result = _calculator.Calculate(request);

            return new AverageIncomeResponse {
                AverageIncome = result
            };
        }

        
    }
}