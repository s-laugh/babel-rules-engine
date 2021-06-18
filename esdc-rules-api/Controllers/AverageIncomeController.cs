using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.AverageIncome;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AverageIncomeController : ControllerBase
    {
        private readonly IHandleAverageIncomeRequests _requestHandler;

        public AverageIncomeController(IHandleAverageIncomeRequests requestHandler)
        {
            _requestHandler = requestHandler;
        }

        /// <summary>
        /// Calculate the Average Weekly income for an individual based on their records of employment and EI Application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public AverageIncomeResponse Calculate(AverageIncomeRequest request)
        {
            var result = _requestHandler.Handle(request);
            return result;
        }
    }
}
