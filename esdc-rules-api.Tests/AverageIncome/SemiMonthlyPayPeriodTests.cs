using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.AverageIncome;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome.Tests
{
    public class SemiMonthlyPayPeriodTests
    {

        [Fact]
        public void ShouldCalculateAverageIncome()
        {
            // Arrange
            var sut = new AverageIncomeCalculator();
            var roe = new SimpleRoe() {
                LastDayForWhichPaid = new DateTime(2021,4,3),
                FinalPayPeriodDay = new DateTime(2021,4,15),
                FirstDayForWhichPaid = new DateTime(2018,6,5),
                PayPeriods = new List<PayPeriod>() {
                    new PayPeriod(1, 200),
                    new PayPeriod(2, 1100),
                    new PayPeriod(3, 1100),
                    new PayPeriod(4, 1000),
                    new PayPeriod(5, 1300),
                    new PayPeriod(6, 1000),
                    new PayPeriod(7, 1100),
                    new PayPeriod(8, 600),
                    new PayPeriod(9, 0),
                    new PayPeriod(10, 0),
                    new PayPeriod(11, 500),
                    new PayPeriod(12, 900),
                    new PayPeriod(13, 900),
                    new PayPeriod(14, 1100),
                    new PayPeriod(15, 1100),
                    new PayPeriod(16, 1200),
                    new PayPeriod(17, 1200),
                    new PayPeriod(18, 1200),
                    new PayPeriod(19, 1300),
                    new PayPeriod(20, 1100),
                    new PayPeriod(21, 1100),
                    new PayPeriod(22, 1100),
                    new PayPeriod(23, 1050),
                    new PayPeriod(24, 1000),
                    new PayPeriod(25, 900),
                },
                PayPeriodType = "semi-monthly"
            };

            var req = new AverageIncomeRequest() {
                Roe = roe,
                NumBestWeeks = 20,
                ApplicationDate = new DateTime(2021,5,2),
            };

            // Act
            var result = sut.Calculate(req);

            // Assert
            Assert.InRange(result, 540, 545);
        }

    }
}