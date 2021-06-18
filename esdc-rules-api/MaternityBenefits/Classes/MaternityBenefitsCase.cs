using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using esdc_rules_api.Lib;

namespace esdc_rules_api.MaternityBenefits.Classes
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
        public decimal MaxWeeklyAmount { get; set; }
        /// <summary>
        /// Percentage of their average income that an applicant is entitled to 
        /// </summary>
        /// <value></value>
        [Required]
        [Range(0, 100)]
        public double Percentage { get; set; }

        /// <summary>
        /// Number of weeks for which an applicant is entitled to receive the benefit
        /// </summary>
        /// <value></value>
        [Required]
        public int NumWeeks { get; set; }
    }
}