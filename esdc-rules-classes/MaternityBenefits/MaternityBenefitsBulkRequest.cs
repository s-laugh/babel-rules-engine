using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.MaternityBenefits
{
    public class MaternityBenefitsBulkRequest
    {
        [Required]
        public MaternityBenefitsCase Rule { get; set; }
        public List<MaternityBenefitsPerson> Persons { get; set; }
    }
}