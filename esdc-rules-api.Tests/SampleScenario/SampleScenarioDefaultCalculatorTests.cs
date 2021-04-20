using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.SampleScenario.Classes;


namespace esdc_rules_api.SampleScenario.Tests
{
    public class SampleScenarioDefaultCalculatorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange

            // Act
            var sut = new SampleScenarioDefaultCalculator();
            
            var rule = new SampleScenarioCase() {
                SomeFactor = 5,
                SomeToggle = true,
                SomeThreshold = 9.99
            };
            var person = new SampleScenarioPerson();
            var result = sut.Calculate(rule, person);

            // Assert
            Assert.Equal(5, result);
        }

    }
}
