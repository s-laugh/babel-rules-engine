using System;
using Microsoft.AspNetCore.Mvc;

using esdc_rules_api.Lib;
using esdc_rules_api.SampleScenario.Classes;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleScenarioController : ControllerBase
    {
        private readonly IHandleRequests<SampleScenarioCase, SampleScenarioPerson> _requestHandler;

        public SampleScenarioController(IHandleRequests<SampleScenarioCase, SampleScenarioPerson> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        [HttpPost]
        public RuleResponse Calculate(RuleRequest<SampleScenarioCase, SampleScenarioPerson> request)
        {
            var result = _requestHandler.Handle(request);
            return result;
        }
    }
}
