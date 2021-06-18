using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using esdc_rules_api.Lib;

namespace esdc_rules_api.MaternityBenefits.Classes
{
    /// <summary>
    /// A class representing an individual applying for maternity benefits
    /// </summary>
    public class MaternityBenefitsPerson : IRulePerson
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <value></value>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Average Weekly Income
        /// </summary>
        /// <value></value>
        [Required]
        public decimal AverageIncome { get; set; }

        public MaternityBenefitsPerson() {
            Id = new Guid();
        }

    }

}