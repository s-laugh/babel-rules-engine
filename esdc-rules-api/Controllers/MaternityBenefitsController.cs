using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        
        private readonly IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> _requestHandler;

        public MaternityBenefitsController(IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        /// <summary>
        /// Calculate the weekly Maternity Benefit entitlement amount given an encoded rule and an individual
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MaternityBenefitsResponse> Calculate(MaternityBenefitsRequest request)
        {
            try {
                var result = _requestHandler.Handle(request);
                return Ok(result);
            } catch (ValidationException ex) {
                return BadRequest(new { error = ex.Message});
            }
        }
    }
}
