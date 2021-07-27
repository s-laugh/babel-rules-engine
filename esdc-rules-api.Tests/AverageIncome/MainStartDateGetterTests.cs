using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_api.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class MainStartDateGetterTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        public void ShouldUseFirstDayForWhichPaidAsAnchor(int dayOfMonth) {
            // Arrange
            var applicationDate = new DateTime(2021, 10, 2);
            var firstDayForWhichPaid = new DateTime(2021, 1, dayOfMonth);

            var sut = new MainStartDateGetter();

            // Act
            var result = sut.Get(firstDayForWhichPaid, applicationDate);

            // Assert
            var expected = new DateTime(2021, 1, 10);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void ShouldUseApplicationDateAsAnchor(int dayOfMonth) {
            // Arrange
            var applicationDate = new DateTime(2021, 10, dayOfMonth);
            var firstDayForWhichPaid = new DateTime(2020, 10, 1);

            var sut = new MainStartDateGetter();

            // Act
            var result = sut.Get(firstDayForWhichPaid, applicationDate);

            // Assert
            var expected = new DateTime(2020, 10, 4);
            Assert.Equal(DayOfWeek.Sunday, result.DayOfWeek);
            Assert.Equal(expected, result);
        }
    }
}