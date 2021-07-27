using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class StartDateFromEndDateGetterTests
    {
        [Theory]
        [InlineData(6)]
        [InlineData(1)]
        public void ShouldWorkForMonthly(int dayOfMonth) {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2019, 3, dayOfMonth, 3, 6, 50);

            // Act
            var result = sut.Get(endDate, ppTypes.MONTHLY);

            // Assert
            var expected = new DateTime(2019, 3, 1, 0, 0, 0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForWeekly() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2019, 3, 14);

            // Act
            var result = sut.Get(endDate, ppTypes.WEEKLY);

            // Assert
            var expected = new DateTime(2019, 3, 8);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForWeeklyOnDifferentMonth() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2021, 9, 4);

            // Act
            var result = sut.Get(endDate, ppTypes.WEEKLY);

            // Assert
            var expected = new DateTime(2021, 8, 29);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForBiWeekly() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2019, 3, 14);

            // Act
            var result = sut.Get(endDate, ppTypes.BIWEEKLY);

            // Assert
            var expected = new DateTime(2019, 3, 1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForBiWeeklyOnDifferentMonth() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2021, 9, 4);

            // Act
            var result = sut.Get(endDate, ppTypes.BIWEEKLY);

            // Assert
            var expected = new DateTime(2021, 8, 22);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForSemiMonthlyOn15() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2021, 9, 15);

            // Act
            var result = sut.Get(endDate, ppTypes.SEMIMONTHLY);

            // Assert
            var expected = new DateTime(2021, 9, 1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForSemiMonthlyOn1() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2021, 8, 31);

            // Act
            var result = sut.Get(endDate, ppTypes.SEMIMONTHLY);

            // Assert
            var expected = new DateTime(2021, 8, 16);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForSemiMonthlyOnYear() {
            // Arrange
            var sut = new StartDateFromEndDateGetter();
            var endDate = new DateTime(2020, 12, 31);

            // Act
            var result = sut.Get(endDate, ppTypes.SEMIMONTHLY);

            // Assert
            var expected = new DateTime(2020, 12, 16);
            Assert.Equal(expected, result);
        }
    }
}