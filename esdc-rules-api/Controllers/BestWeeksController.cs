using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits.Classes;
using esdc_rules_api.BestWeeks;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestWeeksController : ControllerBase
    {
        private readonly IHandleBestWeeksRequests _requestHandler;

        public BestWeeksController(IHandleBestWeeksRequests requestHandler)
        {
            _requestHandler = requestHandler;
        }

        /// <summary>
        /// Calculate the number of best weeks to be used for a person's EI calculation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BestWeeksResponse Calculate(BestWeeksRequest request)
        {
            var result = _requestHandler.Handle(request);
            return result;
        }
    }
}
