using System;
using System.Collections.Generic;
using Xunit;

using esdc_rules_api.MaternityBenefits.Classes;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitsPersonTests
    {
        [Fact]
        public void ShouldGetAverageIncome()
        {
            var weeklyIncome = new List<WeeklyIncome>() {
                new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays(-10),
                    Income = 110m
                },
                new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays(-20),
                    Income = 120m
                },
                new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays(-30),
                    Income = 130m
                },
                new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays(-40),
                    Income = 140m
                },
                new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays(-50),
                    Income = 150m
                },
                new WeeklyIncome() {
                    StartDate = DateTime.Now.AddDays(-1000),
                    Income = 500m
                }, // This one should be ignored
            };

            var person = new MaternityBenefitsPerson() {
                WeeklyIncome = weeklyIncome,
                UnemploymentRegionId = Guid.NewGuid(),
            };

            var result1 = person.GetAverageIncome(1);
            var result2 = person.GetAverageIncome(2);
            var result3 = person.GetAverageIncome(3);

            Assert.Equal(150m, result1);
            Assert.Equal(145m, result2);
            Assert.Equal(140m, result3);
        }
    }
}
