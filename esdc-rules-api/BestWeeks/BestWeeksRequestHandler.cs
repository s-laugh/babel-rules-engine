namespace esdc_rules_api.BestWeeks
{
    public class BestWeeksRequestHandler : IHandleBestWeeksRequests
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