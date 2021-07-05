using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.MaternityBenefits
{
    public class MaternityBenefitsRequest : IRequest
    {
        [Required]
        public MaternityBenefitsPerson Person { get; set; }
        [Required]
        public MaternityBenefitsCase Rule { get; set;}
    }
}