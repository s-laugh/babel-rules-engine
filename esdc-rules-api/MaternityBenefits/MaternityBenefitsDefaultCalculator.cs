using System;
using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits.Classes;

namespace esdc_rules_api.MaternityBenefits
{
    public class MaternityBenefitsDefaultCalculator : ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>
    {
        public decimal Calculate(MaternityBenefitsCase rule, MaternityBenefitsPerson person) {
            int bestWeeks = rule.BestWeeksDict[person.UnemploymentRegionId];
            decimal averageIncome = person.GetAverageIncome(bestWeeks); 

            decimal temp = averageIncome * (decimal)rule.Percentage/100;
            temp = Math.Min(temp, rule.MaxWeeklyAmount);
            decimal amount = temp * rule.NumWeeks;

            return amount;
        }
    }
}