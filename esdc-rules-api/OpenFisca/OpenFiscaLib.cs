using System;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace esdc_rules_api.OpenFisca
{
    public class OpenFiscaLib : IOpenFisca
    {
        private readonly IRestClient _client;

        public OpenFiscaLib(IRestClient client, IOptions<OpenFiscaOptions> optionsAccessor) {
            _client = client;
            _client.BaseUrl = new Uri(optionsAccessor.Value.Url);
            _client.UseNewtonsoftJson();
        }

        public OpenFiscaResource Calculate(OpenFiscaResource request) {
            var restRequest = new RestRequest($"calculate", DataFormat.Json);
            restRequest.AddJsonBody(request);
            var result = _client.Post<OpenFiscaResource>(restRequest);

            if (result.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new OpenFiscaException(result.ErrorMessage);
            }
            return result.Data;
        }
    }
}