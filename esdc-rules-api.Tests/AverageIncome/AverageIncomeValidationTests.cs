using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.Lib;
using esdc_rules_api.AverageIncome;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Tests.AverageIncome
{
    public class AverageIncomeValidationTests
    {
        [Fact]
        public void ShouldFailOnApplicationDate() {
            var sut = new AverageIncomeRequestValidator();

            var req = new AverageIncomeRequest() {
                Roe = new SimpleRoe() {
                    PayPeriods = new List<PayPeriod>() {
                        new PayPeriod(1, 1000)
                    },
                    LastDayForWhichPaid = new DateTime(2020, 11, 5),
                    FinalPayPeriodDay = new DateTime(2020, 11, 30),
                    FirstDayForWhichPaid = new DateTime(2019, 7, 4),
                    PayPeriodType = "weekly"
                },
                ApplicationDate = DateTime.Now.AddDays(10),
                NumBestWeeks = 12
            };

            Action act = () => sut.Validate(req);
            var ex = Assert.Throws<ValidationException>(act);

            Assert.Contains("Application date", ex.Message);
        }

        [Fact]
        public void ShouldPassValidation() {
            var sut = new AverageIncomeRequestValidator();

            var req = new AverageIncomeRequest() {
                Roe = new SimpleRoe() {
                    PayPeriods = new List<PayPeriod>() {
                        new PayPeriod(1, 1000)
                    },
                    LastDayForWhichPaid = new DateTime(2020, 11, 5),
                    FinalPayPeriodDay = new DateTime(2020, 11, 30),
                    FirstDayForWhichPaid = new DateTime(2019, 7, 4),
                    PayPeriodType = "weekly"
                },
                ApplicationDate = new DateTime(2020,12, 1),
                NumBestWeeks = 12
            };

            sut.Validate(req);
        }    
    }
}