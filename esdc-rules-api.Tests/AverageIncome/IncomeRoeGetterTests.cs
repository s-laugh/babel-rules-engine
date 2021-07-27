using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_api.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class IncomeRoeGetterTests
    {
        [Fact]
        public void ShouldWorkForPPOutsideWeek() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 1, 1);
            var maxDate = new DateTime(2021, 12, 31);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 10),
                EndDate = new DateTime(2021, 7, 20)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 700;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPStartMidWeek() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 1, 1);
            var maxDate = new DateTime(2021, 12, 31);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 16),
                EndDate = new DateTime(2021, 7, 31)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 200;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPEndMidWeek() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 1, 1);
            var maxDate = new DateTime(2021, 12, 31);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 1),
                EndDate = new DateTime(2021, 7, 15)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 500;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPWithinWeek() {
            // ** Edge case - shouldn't be allowed 
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 1, 1);
            var maxDate = new DateTime(2021, 12, 31);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 12),
                EndDate = new DateTime(2021, 7, 16)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 500;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPOutsideWeekAndCutoff() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 7, 12);
            var maxDate = new DateTime(2021, 7, 16);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 10),
                EndDate = new DateTime(2021, 7, 20)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 500;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPStartMidWeekAndCutoff() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 1, 1);
            var maxDate = new DateTime(2021, 7, 16);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 15),
                EndDate = new DateTime(2021, 7, 25)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 200;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPEndMidWeekAndCutoff() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 7, 12);
            var maxDate = new DateTime(2021, 12, 31);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 1),
                EndDate = new DateTime(2021, 7, 14)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 300;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldWorkForPPEndMidWeekAndTwoCutoffs() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 7, 12);
            var maxDate = new DateTime(2021, 7, 14);

            var payPeriod = new FullPayPeriod(1, 100) {
                StartDate = new DateTime(2021, 7, 1),
                EndDate = new DateTime(2021, 7, 15)
            };

            var sut = new IncomeRoeGetter();

            // Act
            var result = sut.Get(payPeriod, startOfWeek, minDate, maxDate);

            // Assert
            var expected = 300;
            Assert.Equal(expected, result);
        }

    }
}