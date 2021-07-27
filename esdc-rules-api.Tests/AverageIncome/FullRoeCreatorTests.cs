using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_classes.AverageIncome;
using esdc_rules_api.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class FullRoeCreatorTests
    {
        [Fact]
        public void ShouldWorkNormally() {
            // Arrange
            var simpleRoe = new SimpleRoe() {
                PayPeriodType = ppTypes.MONTHLY,
                FirstDayForWhichPaid = new DateTime(2021, 2, 10),
                LastDayForWhichPaid = new DateTime(2021, 3, 27),
                FinalPayPeriodDay = new DateTime(2021, 3, 31),
                PayPeriods = new List<PayPeriod>(){
                    new PayPeriod(1, 1000),
                    new PayPeriod(2, 2000)
                }
            };

            var startDateGetter = A.Fake<IGetStartDateFromEndDate>();
            var fullPayPeriodCreator = A.Fake<ICreateFullPayPeriods>();

            A.CallTo(() => startDateGetter.Get(new DateTime(2021, 3, 31), ppTypes.MONTHLY))
                .Returns(new DateTime(2021,3,1)); 
            A.CallTo(() => startDateGetter.Get(new DateTime(2021, 2, 28), ppTypes.MONTHLY))
                .Returns(new DateTime(2021,2,1)); 
            
            A.CallTo(() => fullPayPeriodCreator.Create(A<PayPeriod>._, A<DateTime>._, A<DateTime>._, A<DateTime>._, A<DateTime>._ ))
                .Returns(new FullPayPeriod(1, 100))
                .Twice();

            var sut = new FullRoeCreator(startDateGetter, fullPayPeriodCreator);

            // Act
            var result = sut.Create(simpleRoe);

            // Assert
            A.CallTo(() => startDateGetter.Get(new DateTime(2021, 3, 31), ppTypes.MONTHLY))
                .MustHaveHappenedOnceExactly(); 
            
            A.CallTo(() => startDateGetter.Get(new DateTime(2021, 2, 28), ppTypes.MONTHLY))
                .MustHaveHappenedOnceExactly(); 
            
            A.CallTo(() => fullPayPeriodCreator.Create(A<PayPeriod>._, A<DateTime>._, A<DateTime>._, A<DateTime>._, A<DateTime>._ ))
                .MustHaveHappenedTwiceExactly();

            Assert.Equal(2, result.PayPeriods.Count);
        }

    }
}