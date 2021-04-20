using System;
using Xunit;
using FakeItEasy;

using esdc_rules_api.Lib;

namespace esdc_rules_api.MaternityBenefits.Tests
{
    public class RequestHandlerTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            decimal testAmount = 1234.5678m;
            var calculator = A.Fake<ICalculateRules<IRule, IRulePerson>>();

            A.CallTo(() => calculator.Calculate(A<IRule>._, A<IRulePerson>._))
                .Returns(testAmount);

            // Act
            var sut = new RequestHandler<IRule, IRulePerson>(calculator);
            
            var req = A.Fake<RuleRequest<IRule, IRulePerson>>();
            var result = sut.Handle(req);

            // Assert
            Assert.Equal(testAmount, result.Amount);
        }
    }
}
