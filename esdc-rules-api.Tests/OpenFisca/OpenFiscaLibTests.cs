using System;
using Microsoft.Extensions.Options;
using Xunit;
using FakeItEasy;
using RestSharp;

namespace esdc_rules_api.OpenFisca.Tests
{
    public class OpenFiscaLibTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var client = A.Fake<IRestClient>();
            var options = Options.Create(new OpenFiscaOptions() {
                Url = "http://localhost:5000"
            });

            var response = new OpenFiscaResource();
            response.CreatePerson("test_person");
            response.SetProp("test_person", "test_key", 6);

            var postResult = A.Fake<RestResponse<OpenFiscaResource>>();
            postResult.Data = response;
            postResult.StatusCode = System.Net.HttpStatusCode.OK;
            
            A.CallTo(() => client.Execute<OpenFiscaResource>(A<RestRequest>._, Method.POST))
                .Returns(postResult);

            // Act
            var sut = new OpenFiscaLib(client, options);
            var req = new OpenFiscaResource();
            var result = sut.Calculate(req);

            // Assert
            Assert.Equal(6, result.GetProp("test_person", "test_key"));
        }
    }
}
