using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_classes.AverageIncome;
using esdc_rules_api.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class WeeklyIncomeGetterTests
    {
        [Fact]
        public void ShouldWorkNormally() {
            // Arrange
            var startOfWeek = new DateTime(2021, 7, 11);
            var minDate = new DateTime(2021, 1, 1);
            var maxDate = new DateTime(2021, 12, 31);

            var incomeRoeGetter = A.Fake<IGetIncomeFromOneRoe>();

            A.CallTo(() => incomeRoeGetter.Get(A<FullPayPeriod>._, startOfWeek, minDate, maxDate))
                .Returns(100).Once();
            A.CallTo(() => incomeRoeGetter.Get(A<FullPayPeriod>._, startOfWeek, minDate, maxDate))
                .Returns(150).Once();
            
            var sut = new WeeklyIncomeGetter(incomeRoeGetter);

            // Act
            var simpleRoe = new SimpleRoe() {
                LastDayForWhichPaid = maxDate,
                FirstDayForWhichPaid = minDate,
            };
            var payPeriods = new List<FullPayPeriod>() {
                new FullPayPeriod(1, 1000),
                new FullPayPeriod(2, 2000)
            };
            var fullRoe = new FullRoe(simpleRoe, payPeriods);
            var result = sut.Get(fullRoe, startOfWeek);

            // Assert
            A.CallTo(() => incomeRoeGetter.Get(A<FullPayPeriod>._, A<DateTime>._, A<DateTime>._, A<DateTime>._))
                .MustHaveHappenedTwiceExactly();
            
            Assert.Equal(250, result);
        }

    }
}