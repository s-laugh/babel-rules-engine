using System;

namespace esdc_rules_api.Lib
{
    public class RequestHandler<T,U> : IHandleRequests<T,U>
        where T: IRule
        where U: IRulePerson
    {
        private readonly ICalculateRules<T,U> _calculator;

        public RequestHandler(ICalculateRules<T,U> calculator) {
            _calculator = calculator;
        }

        public RuleResponse Handle(RuleRequest<T,U> request) {
            var amount = _calculator.Calculate(request.Rule, request.Person);
            return new RuleResponse() {
                Amount = amount
            };
        }
    }
}