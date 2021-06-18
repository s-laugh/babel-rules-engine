using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome.Tests
{
    public class AverageIncomeCalculatorTests
    {

        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var sut = new AverageIncomeCalculator();
            var roe = new SimpleRoe() {
                LastDayForWhichPaid = new DateTime(2021,4,30),
                FinalPayPeriodDay = new DateTime(2021,4,17),
                FirstDayForWhichPaid = new DateTime(2018,6,5),
                PayPeriods = new List<PayPeriod>() {
                    new PayPeriod(1, 2300),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 2500),
                    new PayPeriod(1, 3100),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 3000),
                    new PayPeriod(1, 2900),
                    new PayPeriod(1, 3400),
                    new PayPeriod(1, 3400),
                },
                PayPeriodType = "monthly"
            };

            var req = new AverageIncomeRequest() {
                Roe = roe,
                NumBestWeeks = 14,
                ApplicationDate = new DateTime(2021,5,2),
            };

            // Act
            var result = sut.Calculate(req);

            // Assert
            Assert.InRange(result, 500, 600);
        }
    }
}