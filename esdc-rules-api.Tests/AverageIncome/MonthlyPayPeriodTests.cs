using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.AverageIncome;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome.Tests
{
    public class MonthlyPayPeriodTests
    {

        [Fact]
        public void ShouldCalculateAverageIncome()
        {
            // Arrange
            var sut = new AverageIncomeCalculator();
            var roe = new SimpleRoe() {
                LastDayForWhichPaid = new DateTime(2021,4,17),
                FinalPayPeriodDay = new DateTime(2021,4,30),
                FirstDayForWhichPaid = new DateTime(2018,6,5),
                PayPeriods = new List<PayPeriod>() {
                    new PayPeriod(1, 2300),
                    new PayPeriod(2, 3400),
                    new PayPeriod(3, 3400),
                    new PayPeriod(4, 3400),
                    new PayPeriod(5, 2500),
                    new PayPeriod(6, 3100),
                    new PayPeriod(7, 3400),
                    new PayPeriod(8, 3400),
                    new PayPeriod(9, 3400),
                    new PayPeriod(10, 3000),
                    new PayPeriod(11, 2900),
                    new PayPeriod(12, 3400),
                    new PayPeriod(13, 3400),
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
            Assert.InRange(result, 830, 835);
        }

    }
}