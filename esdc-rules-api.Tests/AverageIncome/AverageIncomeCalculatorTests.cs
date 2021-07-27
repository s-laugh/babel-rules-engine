using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using esdc_rules_classes.AverageIncome;
using esdc_rules_api.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class AverageIncomeCalculatorTests
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

            var fullRoeCreator = A.Fake<ICreateFullRoes>();
            var incomeListGetter = A.Fake<IGetIncomeList>();

            var fullRoe = A.Fake<FullRoe>();
            var incomeList = new List<decimal>() { 3, -33.3M, 9, 9.999M, 10, 14, 8.8M, 16.5M, 7 };

            A.CallTo(() => fullRoeCreator.Create(A<SimpleRoe>._))
                .Returns(fullRoe); 

            A.CallTo(() => incomeListGetter.Get(A<DateTime>._, A<FullRoe>._))
                .Returns(incomeList); 


            var sut = new AverageIncomeCalculator(fullRoeCreator, incomeListGetter);

            // Act
            var req = new AverageIncomeRequest() {
                NumBestWeeks = 3,
                Roe = A.Fake<SimpleRoe>(),
                ApplicationDate = DateTime.Now,
            };
            var result = sut.Calculate(req);

            // Assert
            A.CallTo(() => fullRoeCreator.Create(A<SimpleRoe>._))
                .MustHaveHappenedOnceExactly(); 
            
            A.CallTo(() => incomeListGetter.Get(req.ApplicationDate, A<FullRoe>._))
                .MustHaveHappenedOnceExactly(); 

            Assert.Equal(13.5M, result);
        }

    }
}