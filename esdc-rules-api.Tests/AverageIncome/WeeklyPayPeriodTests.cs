using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.AverageIncome;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome.Tests
{
    public class WeeklyPayPeriodTests
    {

        [Fact]
        public void ShouldCalculateAverageIncome()
        {
            // Arrange
            var sut = new AverageIncomeCalculator();
            var roe = new SimpleRoe() {
                LastDayForWhichPaid = new DateTime(2020,9,18),
                FinalPayPeriodDay = new DateTime(2020,9,19),
                FirstDayForWhichPaid = new DateTime(2019,4,6),
                PayPeriods = new List<PayPeriod>() {
                    new PayPeriod(1, 500),
                    new PayPeriod(2, 530),
                    new PayPeriod(3, 520),
                    new PayPeriod(4, 510),
                    new PayPeriod(5, 0),
                    new PayPeriod(6, 450),
                    new PayPeriod(7, 470),
                    new PayPeriod(8, 520),
                    new PayPeriod(9, 520),
                    new PayPeriod(10, 520),
                    new PayPeriod(11, 520),
                    new PayPeriod(12, 520),
                    new PayPeriod(13, 520),
                    new PayPeriod(14, 540),
                    new PayPeriod(15, 500),
                    new PayPeriod(16, 550),
                    new PayPeriod(17, 0),
                    new PayPeriod(18, 450),
                    new PayPeriod(19, 500),
                    new PayPeriod(20, 500),
                    new PayPeriod(21, 500),
                    new PayPeriod(22, 500),
                    new PayPeriod(23, 520),
                    new PayPeriod(24, 520),
                    new PayPeriod(25, 520),
                    new PayPeriod(26, 580),
                    new PayPeriod(27, 580),
                    new PayPeriod(28, 600),
                    new PayPeriod(29, 600),
                    new PayPeriod(30, 610),
                    new PayPeriod(31, 610),
                    new PayPeriod(32, 610),
                    new PayPeriod(33, 610),
                    new PayPeriod(34, 610),
                    new PayPeriod(35, 630),
                    new PayPeriod(36, 630),
                    new PayPeriod(37, 630),
                    new PayPeriod(38, 630),
                    new PayPeriod(39, 630),
                    new PayPeriod(40, 700),
                    new PayPeriod(41, 700),
                    new PayPeriod(42, 700),
                    new PayPeriod(43, 700),
                    new PayPeriod(44, 700),
                    new PayPeriod(45, 700),
                    new PayPeriod(46, 700),
                    new PayPeriod(47, 700),
                    new PayPeriod(48, 700),
                    new PayPeriod(49, 700),
                    new PayPeriod(50, 700),
                    new PayPeriod(51, 700),
                    new PayPeriod(52, 700),
                    new PayPeriod(53, 750)
                },
                PayPeriodType = "weekly"
            };

            var req = new AverageIncomeRequest() {
                Roe = roe,
                NumBestWeeks = 14,
                ApplicationDate = new DateTime(2020,10,4),
            };

            // Act
            var result = sut.Calculate(req);

            // Assert
            Assert.InRange(result, 680, 690);
        }

    }
}