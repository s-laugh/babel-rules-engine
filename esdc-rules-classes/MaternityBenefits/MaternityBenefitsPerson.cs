using System;

namespace esdc_rules_classes.MaternityBenefits
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
        public Guid Id { get; set; }

        /// <summary>
        /// Average Weekly Income
        /// </summary>
        /// <value></value>
        public decimal AverageIncome { get; set; }

        public MaternityBenefitsPerson() {
            Id = new Guid();
        }

    }

}