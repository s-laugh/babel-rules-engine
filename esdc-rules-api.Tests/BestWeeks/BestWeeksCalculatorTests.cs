using System;
using System.Collections.Generic;
using Xunit;

namespace esdc_rules_api.BestWeeks.Tests
{
    public class BestWeeksCalculatorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var sut = new BestWeeksCalculator();
            var postalCode = "A1A1A1";

            // Act
            var result = sut.Calculate(postalCode);

            // Assert
            Assert.InRange<int>(result, 14, 22);
        }
    }
}
