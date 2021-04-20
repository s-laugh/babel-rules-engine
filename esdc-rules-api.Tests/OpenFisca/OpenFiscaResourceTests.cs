using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace esdc_rules_api.OpenFisca.Tests
{
    public class OpenFiscaResourceTests
    {
        [Fact]
        public void ShouldCreateKeys()
        {
            // Arrange
            string personKey = "myPersonKey";
            string propKey = "myPropKey";
            decimal value = 99.23223m;

            // Act
            var sut = new OpenFiscaResource();
            sut.CreatePerson(personKey);
            sut.SetProp(personKey, propKey, value);

            // Assert
            var manualResult = sut.persons[personKey][propKey].First().Value;
            var getResult = sut.GetProp(personKey, propKey);
            Assert.Equal(value, manualResult);
            Assert.Equal(value, getResult);
        }

        [Fact]
        public void ShouldFetchKeys()
        {
            // Arrange
            string personKey = "myPersonKey";
            string propKey = "myPropKey";
            decimal value = 99.23223m;

            // Act
            var sut = new OpenFiscaResource();
            sut.persons.Add(personKey, new Dictionary<string, Dictionary<string, object>>());
            sut.persons[personKey].Add(propKey, new Dictionary<string, object>());
            sut.persons[personKey][propKey].Add("dateKey", value);

            var result = sut.GetProp(personKey, propKey);

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ShouldFetchKeysFromManualBuild()
        {
            // Arrange
            string personKey = "myPersonKey";
            string propKey = "myPropKey";
            decimal value = 99.23223m;

            // Act
            var sut = BuildManualRequest(personKey, propKey, value);
            var result = sut.GetProp(personKey, propKey);

            // Assert
            Assert.Equal(value, result);
        }

        private OpenFiscaResource BuildManualRequest(string personKey, string propKey, object value) {
            return new OpenFiscaResource() {
                persons = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>() {
                    {
                      personKey, 
                      new Dictionary<string, Dictionary<string, object>>() {
                          {
                              propKey,
                              new Dictionary<string, object>() {
                                  { "2020-09-25", value }
                              }
                          }
                      }
                    }
                }
            };
        }

    }
}
