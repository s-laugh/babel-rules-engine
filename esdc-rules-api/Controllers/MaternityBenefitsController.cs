using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits.Classes;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        private readonly IHandleRequests<MaternityBenefitsCase, MaternityBenefitsPerson> _requestHandler;

        public MaternityBenefitsController(IHandleRequests<MaternityBenefitsCase, MaternityBenefitsPerson> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        [HttpPost]
        public RuleResponse Calculate(RuleRequest<MaternityBenefitsCase, MaternityBenefitsPerson> request)
        {
            var result = _requestHandler.Handle(request);
            return result;
        }
    }
}
