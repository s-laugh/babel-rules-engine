using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits;
using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        private readonly IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> _requestHandler;
        private readonly IHandleBulkRequests _bulkRequestHandler;

        public MaternityBenefitsController(
            IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> requestHandler,
            IHandleBulkRequests bulkRequestHandler
            )
        {
            _requestHandler = requestHandler;
            _bulkRequestHandler = bulkRequestHandler;
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
        
        /// <summary>
        /// Calculate the weekly Maternity Benefit entitlement amount given an encoded rule and a set of individuals
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Bulk")]
        public ActionResult<MaternityBenefitsBulkResponse> CalculateBulk(MaternityBenefitsBulkRequest request) {
            try {
                var result = _bulkRequestHandler.Handle(request);
                return Ok(result);
            } catch (ValidationException ex) {
                return BadRequest(new { error = ex.Message});
            }
        }
    }
}
