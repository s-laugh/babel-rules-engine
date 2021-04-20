using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Xunit;
using FakeItEasy;
using RestSharp;

using esdc_rules_api.MaternityBenefits.Classes;
using OF = esdc_rules_api.MaternityBenefits.OpenFiscaMaternityBenefitsVariables;
using esdc_rules_api.OpenFisca;


namespace esdc_rules_api.MaternityBenefits.Tests
{
    public class MaternityBenefitsOpenFiscaCalculatorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var testAmount = 5555;
            var regionId = Guid.NewGuid();
            var openFiscaLib = A.Fake<IOpenFisca>();

            var openFiscaResult = new OpenFiscaResource();
            openFiscaResult.CreatePerson("test_person");
            openFiscaResult.SetProp("test_person", OF.MaternityBenefitsAmount, testAmount);

            A.CallTo(() => openFiscaLib.Calculate(A<OpenFiscaResource>._))
                .Returns(openFiscaResult);

            // Act
            var sut = new MaternityBenefitsOpenFiscaCalculator(openFiscaLib);
            
            var rule = new MaternityBenefitsCase() {
                MaxWeeklyAmount = 500,
                NumWeeks = 10,
                Percentage = 50,
                BestWeeksDict = new Dictionary<Guid, int>() {
                    {regionId, 5}
                }
            };
            var person = GeneratePerson(regionId);
            var result = sut.Calculate(rule, person);

            // Assert
            Assert.Equal(testAmount, result);
        }

        private MaternityBenefitsPerson GeneratePerson(Guid regionId) {
            var weeklyIncomes = new List<WeeklyIncome>();

            for (int i = 1; i <= 10; i++) {
                weeklyIncomes.Add(new WeeklyIncome() {
                    StartDate = DateTime.Now.AddMonths(-i),
                    Income = 100*i
                });
            }

            // Ignored
            weeklyIncomes.Add(new WeeklyIncome() {
                StartDate = DateTime.Now.AddDays(5),
                Income = 2000
            });
            weeklyIncomes.Add(new WeeklyIncome() {
                StartDate = DateTime.Now.AddYears(-2),
                Income = 2000
            });

            var person = new MaternityBenefitsPerson() {
                UnemploymentRegionId = regionId,
                WeeklyIncome = weeklyIncomes
            };

            return person;
        }
    }
}
