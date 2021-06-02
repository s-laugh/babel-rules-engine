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
        /// Economic/Unemployment region identifier
        /// </summary>
        /// <value></value>
        [Required]
        public Guid UnemploymentRegionId { get; set; }
        /// <summary>
        /// Collection of weekly income amounts
        /// </summary>
        /// <value></value>
        [Required]
        public List<WeeklyIncome> WeeklyIncome { get; set; }

        public MaternityBenefitsPerson() {
            Id = new Guid();
            UnemploymentRegionId = new Guid();
            WeeklyIncome = new List<WeeklyIncome>();
        }

        public decimal GetAverageIncome(int divisor) {
            if (divisor <= 0) {
                throw new Exception("Divisor must be greater than 0");
            }   
            
            var oneYearAgo = DateTime.Now.AddYears(-1);

            var lastYearsIncome = WeeklyIncome.Where(x => x.StartDate > oneYearAgo && x.StartDate < DateTime.Now);
            var averageIncome = lastYearsIncome
                .OrderByDescending(x => x.Income)
                .Take(divisor)
                .Average(x => x.Income);

            return averageIncome;
        }
    }

    /// <summary>
    /// Class representing a weekly pay period for an applicant
    /// </summary>
    public class WeeklyIncome
    {
        /// <summary>
        /// Start date of pay period
        /// </summary>
        /// <value></value>
        [Required]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Amount of income during the weekly pay period
        /// </summary>
        /// <value></value>
        [Required]
        public decimal Income { get; set; }
    }

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