using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AverageIncomeController : ControllerBase
    {
        private readonly IHandleRequests<AverageIncomeRequest, AverageIncomeResponse> _requestHandler;

        public AverageIncomeController(IHandleRequests<AverageIncomeRequest, AverageIncomeResponse> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        /// <summary>
        /// Calculate the Average Weekly income for an individual based on their records of employment and EI Application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AverageIncomeResponse> Calculate(AverageIncomeRequest request)
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
