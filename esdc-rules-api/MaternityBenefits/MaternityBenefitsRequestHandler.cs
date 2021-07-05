using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits;

using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.MaternityBenefits
{
    public class MaternityBenefitsRequestHandler : IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> 
    {
        private readonly ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson> _calculator;
        public MaternityBenefitsRequestHandler(ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson> calculator) {
            _calculator = calculator;
        }
        public MaternityBenefitsResponse Handle(MaternityBenefitsRequest request) {
            var result = _calculator.Calculate(request.Rule, request.Person);

            return new MaternityBenefitsResponse() {
                Amount = result
            };
        }
    }
}