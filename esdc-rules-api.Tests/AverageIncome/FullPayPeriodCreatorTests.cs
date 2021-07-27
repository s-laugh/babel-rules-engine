using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_classes.AverageIncome;
using esdc_rules_api.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class FullPayPeriodCreatorTests
    {
        [Theory]
        [InlineData(1, 1000)] // 1 day
        [InlineData(2, 500)] // 2 day
        [InlineData(4, 250)] // 4 day
        [InlineData(10, 100)] // 10 day
        public void ShouldWorkOnSimpleCases(int endDay, decimal expectedAmount) {
            // Arrange
            var minDate = new DateTime(2020, 1, 1);
            var maxDate = new DateTime(2020, 12, 31);
            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, endDay);
            var sut = new FullPayPeriodCreator();

            // Act
            var simplePP = new PayPeriod(1, 1000);
            var result = sut.Create(simplePP, startDate, endDate, minDate, maxDate);

            // Assert
            Assert.Equal(expectedAmount, result.Amount);
        }

        [Fact]
        public void ShouldUseMinimumDate() {
            // Arrange
            var minDate = new DateTime(2020, 1, 5);
            var maxDate = new DateTime(2020, 12, 31);
            var sut = new FullPayPeriodCreator();

            // Act
            var simplePP = new PayPeriod(1, 1000);
            var startDate = new DateTime(2020, 1, 1); // Will get cut off by min date
            var endDate = new DateTime(2020, 1, 8);
            var result = sut.Create(simplePP, startDate, endDate, minDate, maxDate);

            // Assert
            decimal expectedAmount = 250;
            Assert.Equal(expectedAmount, result.Amount);
        }

        [Fact]
        public void ShouldUseMaximumDate() {
            // Arrange
            var minDate = new DateTime(2020, 1, 1);
            var maxDate = new DateTime(2020, 12, 19);
            var sut = new FullPayPeriodCreator();

            // Act
            var simplePP = new PayPeriod(1, 1000);
            var startDate = new DateTime(2020, 12, 10); 
            var endDate = new DateTime(2020, 12, 25); // Will get cut off by max date
            var result = sut.Create(simplePP, startDate, endDate, minDate, maxDate);

            // Assert
            decimal expectedAmount = 100;
            Assert.Equal(expectedAmount, result.Amount);
        }

        [Fact]
        public void ShouldUseMinAndMaxDates() {
            // Arrange
            var minDate = new DateTime(2020, 1, 10);
            var maxDate = new DateTime(2020, 1, 19);
            var sut = new FullPayPeriodCreator();

            // Act
            var simplePP = new PayPeriod(1, 1000);
            var startDate = new DateTime(2020, 1, 8); 
            var endDate = new DateTime(2020, 1, 25);
            var result = sut.Create(simplePP, startDate, endDate, minDate, maxDate);

            // Assert
            decimal expectedAmount = 100;
            Assert.Equal(expectedAmount, result.Amount);
        }
    }
}