using System;
using Xunit;
using FakeItEasy;

using esdc_rules_classes.MaternityBenefits;
using OF = esdc_rules_api.MaternityBenefits.OpenFiscaMaternityBenefitsVariables;
using esdc_rules_api.OpenFisca;


namespace esdc_rules_api.MaternityBenefits.Tests
{
    public class MaternityBenefitsOpenFiscaCalculatorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var testAmount = 5555;
            var regionId = Guid.NewGuid();
            var openFiscaLib = A.Fake<IOpenFisca>();

            var openFiscaResult = new OpenFiscaResource();
            openFiscaResult.CreatePerson("test_person");
            openFiscaResult.SetProp("test_person", OF.MaternityBenefitsAmount, testAmount);

            A.CallTo(() => openFiscaLib.Calculate(A<OpenFiscaResource>._))
                .Returns(openFiscaResult);

            // Act
            var sut = new MaternityBenefitsOpenFiscaCalculator(openFiscaLib);
            
            var rule = new MaternityBenefitsCase() {
                MaxWeeklyAmount = 500,
                NumWeeks = 10,
                Percentage = 50,
            };
            var person = new MaternityBenefitsPerson() {
                AverageIncome = 1000
            };
            var result = sut.Calculate(rule, person);

            // Assert
            Assert.Equal(testAmount, result);
        }
    }
}
