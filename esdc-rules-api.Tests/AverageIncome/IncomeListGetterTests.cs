using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_classes.AverageIncome;
using esdc_rules_api.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class IncomeListGetterTests
    {
        [Fact]
        public void ShouldWorkNormally() {
            // Arrange
            var applicationDate = new DateTime(2021, 6, 22);
            var startDate = new DateTime(2021, 6, 1);

            var mainStartDateGetter = A.Fake<IGetMainStartDate>();
            var weeklyIncomeGetter = A.Fake<IGetIncomeForOneWeek>();

            A.CallTo(() => mainStartDateGetter.Get(A<DateTime>._, A<DateTime>._))
                .Returns(startDate);

            A.CallTo(() => weeklyIncomeGetter.Get(A<FullRoe>._, A<DateTime>._))
                .Returns(10);
            
            var sut = new IncomeListGetter(mainStartDateGetter, weeklyIncomeGetter);

            // Act
            var simpleRoe = new SimpleRoe() {
                LastDayForWhichPaid = new DateTime(2021, 6, 21),
                FirstDayForWhichPaid = new DateTime(2021, 1, 1),
            };
            var payPeriods = new List<FullPayPeriod>() {
                new FullPayPeriod(1, 1000),
                new FullPayPeriod(2, 2000)
            };
            var fullRoe = new FullRoe(simpleRoe, payPeriods);
            var result = sut.Get(applicationDate, fullRoe);

            // Assert
            A.CallTo(() => mainStartDateGetter.Get(A<DateTime>._, A<DateTime>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => weeklyIncomeGetter.Get(A<FullRoe>._, A<DateTime>._))
                .MustHaveHappened(3, Times.Exactly);
            
            Assert.Equal(3, result.Count);
        }

    }
}