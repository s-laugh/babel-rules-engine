using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.AverageIncome
{
    public class AverageIncomeRequest : IRequest
    {
        [Required]
        public SimpleRoe Roe { get; set; }
        [Required]
        public DateTime ApplicationDate { get; set;}
        [Required]
        [Range(1,52)]
        public int NumBestWeeks { get; set; }
    }
}