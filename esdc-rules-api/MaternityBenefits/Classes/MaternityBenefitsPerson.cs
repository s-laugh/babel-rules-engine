using System;
using System.Collections.Generic;
using System.Linq;

using esdc_rules_api.Lib;

namespace esdc_rules_api.MaternityBenefits.Classes
{
    public class MaternityBenefitsPerson : IRulePerson
    {
        public Guid Id { get; set; }
        public Guid UnemploymentRegionId { get; set; }
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

    public class WeeklyIncome
    {
        public DateTime StartDate { get; set; }
        public decimal Income { get; set; }
    }

    public class UnemploymentRegion
    {
        public Guid Id { get; set; }
        public int BestWeeks { get; set; }

        public UnemploymentRegion() {
            Id = new Guid();
            BestWeeks = 0;
        }
    }
}