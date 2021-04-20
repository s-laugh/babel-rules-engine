using System;
using System.Linq;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits.Classes;
using esdc_rules_api.OpenFisca;

using OF = esdc_rules_api.MaternityBenefits.OpenFiscaMaternityBenefitsVariables;

namespace esdc_rules_api.MaternityBenefits
{
    public class MaternityBenefitsOpenFiscaCalculator : ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>
    {
        private readonly IOpenFisca _openFiscaLib;

        public MaternityBenefitsOpenFiscaCalculator(IOpenFisca openFiscaLib) {
            _openFiscaLib = openFiscaLib;
        }

        public decimal Calculate(MaternityBenefitsCase rule, MaternityBenefitsPerson person) {
            var request = BuildRequest(rule, person);
            var openFiscaResponse = _openFiscaLib.Calculate(request);
            var result = ExtractResponse(openFiscaResponse);

            return result;
        }

        private OpenFiscaResource BuildRequest(MaternityBenefitsCase rule, MaternityBenefitsPerson person) {
            var result = new OpenFiscaResource();
            string personName = "person1";
            result.CreatePerson(personName);

            // Target value 
            result.SetProp(personName, OF.MaternityBenefitsAmount, null);

            // Person vars
            int bestWeeks = rule.BestWeeksDict[person.UnemploymentRegionId];
            decimal averageIncome = person.GetAverageIncome(bestWeeks);
            result.SetProp(personName, OF.AverageIncome, averageIncome);

            // Parameter overrides
            result.SetProp(personName, OF.MaxWeeklyAmount, rule.MaxWeeklyAmount);
            result.SetProp(personName, OF.NumWeeks, rule.NumWeeks);
            result.SetProp(personName, OF.Percentage, rule.Percentage);
            
            return result;
        }

        private decimal ExtractResponse(OpenFiscaResource openFiscaResponse) {
            var personKey = openFiscaResponse.persons.First().Key;
            var dictResult = openFiscaResponse.GetProp(personKey, OF.MaternityBenefitsAmount);
            return Convert.ToDecimal(dictResult);
        }
    }
}