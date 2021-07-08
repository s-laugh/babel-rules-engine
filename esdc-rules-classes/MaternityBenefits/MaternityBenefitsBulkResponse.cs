using System;
using System.Collections.Generic;

namespace esdc_rules_classes.MaternityBenefits
{
    public class MaternityBenefitsBulkResponse
    {
        public Dictionary<Guid, MaternityBenefitsResponse> ResponseDict { get; set; }
    }
}