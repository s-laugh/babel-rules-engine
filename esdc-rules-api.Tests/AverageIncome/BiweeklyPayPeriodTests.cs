using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.AverageIncome;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome.Tests
{
    public class BiweeklyPayPeriodTests
    {

        [Fact]
        public void ShouldCalculateAverageIncome()
        {
            // Arrange
            var sut = new AverageIncomeCalculator();
            var roe = new SimpleRoe() {
                LastDayForWhichPaid = new DateTime(2020,9,18),
                FinalPayPeriodDay = new DateTime(2020,9,19),
                FirstDayForWhichPaid = new DateTime(2020,4,6),
                PayPeriods = new List<PayPeriod>() {
                    new PayPeriod(1, 1800),
                    new PayPeriod(2, 1800),
                    new PayPeriod(3, 1800),
                    new PayPeriod(4, 750),
                    new PayPeriod(5, 0),
                    new PayPeriod(6, 450),
                    new PayPeriod(7, 1800),
                    new PayPeriod(8, 1800),
                    new PayPeriod(9, 1800),
                    new PayPeriod(10, 1800),
                    new PayPeriod(11, 1800),
                    new PayPeriod(12, 1100)
                },
                PayPeriodType = "bi-weekly"
            };

            var req = new AverageIncomeRequest() {
                Roe = roe,
                NumBestWeeks = 20,
                ApplicationDate = new DateTime(2020,10,4),
            };

            // Act
            var result = sut.Calculate(req);

            // Assert
            Assert.InRange(result, 810, 815);
        }

    }
}