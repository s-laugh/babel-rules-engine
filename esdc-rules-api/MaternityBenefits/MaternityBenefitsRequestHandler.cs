using Microsoft.Extensions.Caching.Memory;

using esdc_rules_api.Lib;
using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.MaternityBenefits
{
    public class MaternityBenefitsRequestHandler : IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> 
    {
        private readonly IMemoryCache _cache;
        private readonly ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson> _calculator;
        public MaternityBenefitsRequestHandler(
            IMemoryCache cache,
            ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson> calculator) {
            _cache = cache;
            _calculator = calculator;
        }

        public MaternityBenefitsResponse Handle(MaternityBenefitsRequest request) {
            decimal result;
            var cacheKey = BuildCacheKey(request);
            if (_cache.TryGetValue(cacheKey, out decimal cacheAmount)) {
                result = cacheAmount;
            } else {
                result = _calculator.Calculate(request.Rule, request.Person);
                _cache.Set<decimal>(cacheKey, result);
            }
            
            return new MaternityBenefitsResponse() {
                Amount = result
            };
        }

        private string BuildCacheKey(MaternityBenefitsRequest request) {
            var r = request.Rule;
            return $"{request.Person.Id}_{r.MaxWeeklyAmount}_{r.NumWeeks}_{r.Percentage}";
        }
    }
}