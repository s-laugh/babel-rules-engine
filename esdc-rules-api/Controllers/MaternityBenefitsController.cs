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

        /// <summary>
        /// Calculate the Maternity Benefit entitlement amount given an encoded rule and an individual
        /// </summary>
        /// <param name="request">Request object contains an encoding of the maternity benefit case that should be executed, as well as the individual applicant that the case will be run against</param>
        /// <returns></returns>
        [HttpPost]
        public RuleResponse Calculate(RuleRequest<MaternityBenefitsCase, MaternityBenefitsPerson> request)
        {
            var result = _requestHandler.Handle(request);
            return result;
        }
    }
}
