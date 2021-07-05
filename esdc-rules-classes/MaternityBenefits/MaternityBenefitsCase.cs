using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.MaternityBenefits
{
    /// <summary>
    /// A class representing a maternity benefit calculation rule encoding.
    /// </summary>
    public class MaternityBenefitsCase : IRule
    {
        /// <summary>
        ///  Maximum that an applicant is entitled to on a weekly basis
        /// </summary>
        /// <value></value>
        [Required]
        [Range(100,1000)]
        public decimal MaxWeeklyAmount { get; set; }
        /// <summary>
        /// Percentage of their average income that an applicant is entitled to 
        /// </summary>
        /// <value></value>
        [Required]
        [Range(10,100)]
        public double Percentage { get; set; }

        /// <summary>
        /// Number of weeks for which an applicant is entitled to receive the benefit
        /// </summary>
        /// <value></value>
        [Required]
        [Range(10,30)]
        public int NumWeeks { get; set; }
    }
}