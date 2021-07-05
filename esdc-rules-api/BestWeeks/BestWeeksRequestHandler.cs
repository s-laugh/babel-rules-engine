using esdc_rules_api.Lib;
using esdc_rules_classes.BestWeeks;

namespace esdc_rules_api.BestWeeks
{
    public class BestWeeksRequestHandler : IHandleRequests<BestWeeksRequest, BestWeeksResponse>
    {
        private readonly ICalculateBestWeeks _calculator;

        public BestWeeksRequestHandler(ICalculateBestWeeks calculator)
        {
            _calculator = calculator;
        }

        public BestWeeksResponse Handle(BestWeeksRequest request) {
            var result = _calculator.Calculate(request.PostalCode);
            return new BestWeeksResponse() {
                NumBestWeeks = result
            };
        }
    }
}