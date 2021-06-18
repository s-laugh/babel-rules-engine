using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_api.BestWeeks
{
    /// <summary>
    /// Class representing an umemployment/economic region
    /// </summary>
    public class UnemploymentRegion
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <value></value>
        [Required]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Number of best weeks that will be used in the calculation of entitlement for someone in this region
        /// </summary>
        /// <value></value>
        [Required]
        [Range(0, 52)]
        public int BestWeeks { get; set; }

        public UnemploymentRegion() {
            Id = new Guid();
            BestWeeks = 0;
        }
    }
}