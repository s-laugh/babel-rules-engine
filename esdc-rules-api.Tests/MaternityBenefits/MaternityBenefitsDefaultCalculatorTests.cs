using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.MaternityBenefits.Classes;


namespace esdc_rules_api.MaternityBenefits.Tests
{
    public class MaternityBenefitsDefaultCalculatorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var regionId = Guid.NewGuid();

            // Act
            var sut = new MaternityBenefitsDefaultCalculator();
            
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
            Assert.Equal(4000, result);
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
