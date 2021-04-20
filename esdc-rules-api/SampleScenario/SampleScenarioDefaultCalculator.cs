using System;

using esdc_rules_api.Lib;
using esdc_rules_api.SampleScenario.Classes;

namespace esdc_rules_api.SampleScenario
{
    public class SampleScenarioDefaultCalculator : ICalculateRules<SampleScenarioCase, SampleScenarioPerson>
    {
        public decimal Calculate(SampleScenarioCase rule, SampleScenarioPerson person) {
            decimal amount = 0m;
            if (rule.SomeToggle) {
                amount = rule.SomeFactor;
            }

            if (Convert.ToDouble(amount) > rule.SomeThreshold) {
                amount = 0;
            }

            return amount;
        }
    }
}