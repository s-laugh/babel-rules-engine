using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_classes.MaternityBenefits;


namespace esdc_rules_api.MaternityBenefits.Tests
{
    public class MaternityBenefitsDefaultCalculatorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange

            // Act
            var sut = new MaternityBenefitsDefaultCalculator();
            
            var rule = new MaternityBenefitsCase() {
                MaxWeeklyAmount = 500,
                Percentage = 55,
                NumWeeks = 10
            };
            var person = new MaternityBenefitsPerson() {
                AverageIncome = 1000
            };
            var result = sut.Calculate(rule, person);

            // Assert
            Assert.Equal(5000, result);
        }
    }
}
