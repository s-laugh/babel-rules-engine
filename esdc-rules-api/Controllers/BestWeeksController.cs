using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_classes.BestWeeks;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestWeeksController : ControllerBase
    {
        private readonly IHandleRequests<BestWeeksRequest, BestWeeksResponse> _requestHandler;

        public BestWeeksController(IHandleRequests<BestWeeksRequest, BestWeeksResponse> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        /// <summary>
        /// Calculate the number of best weeks to be used for a person's EI calculation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BestWeeksResponse> Calculate(BestWeeksRequest request)
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
