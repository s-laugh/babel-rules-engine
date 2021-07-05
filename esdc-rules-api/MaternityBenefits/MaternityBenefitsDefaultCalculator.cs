using System;
using esdc_rules_api.Lib;
using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.MaternityBenefits
{
    public class MaternityBenefitsDefaultCalculator : ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>
    {
        public decimal Calculate(MaternityBenefitsCase rule, MaternityBenefitsPerson person) {
            decimal temp = person.AverageIncome * (decimal)rule.Percentage/100;
            var weeklyAmount = Math.Min(temp, rule.MaxWeeklyAmount);
            return weeklyAmount * rule.NumWeeks;
        }
    }
}