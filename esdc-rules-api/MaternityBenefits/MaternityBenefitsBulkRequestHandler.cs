using System.Collections.Generic;

using esdc_rules_api.Lib;
using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.MaternityBenefits
{
    public class MaternityBenefitsBulkRequestHandler : IHandleBulkRequests 
    {
        private readonly IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> _requestHandler;
        public MaternityBenefitsBulkRequestHandler(
            IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> requestHandler
        ) {
            _requestHandler = requestHandler;
        }
        
        public MaternityBenefitsBulkResponse Handle(MaternityBenefitsBulkRequest request) {
            var dict = new Dictionary<System.Guid, MaternityBenefitsResponse>();

            foreach (var p in request.Persons) {
                var singleRequest = new MaternityBenefitsRequest() {
                    Rule = request.Rule,
                    Person = p
                };
                var nextResult = _requestHandler.Handle(singleRequest);
                dict.Add(p.Id, nextResult);
            }

            return new MaternityBenefitsBulkResponse() {
                ResponseDict = dict
            };
        }
    }
}