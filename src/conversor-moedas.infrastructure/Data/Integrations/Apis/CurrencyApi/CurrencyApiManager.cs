using conversor_moedas.domain.Integrations.Api.CurrencyApi;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace conversor_moedas.infrastructure.Data.Integrations.Apis.CurrencyApi
{
    public class CurrencyApiManager : ICurrencyApiManager
    {
        private readonly IConfiguration _configuration;

        public CurrencyApiManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<decimal> GetCurrencyValue(string currencyTo, string currencyFrom)
        {
            var url = _configuration.GetSection("CurrencyApi:Url").Value;
            var apiKey = _configuration.GetSection("CurrencyApi:ApiKey").Value;
            var completeUrl = $"{url}/latest?apikey={apiKey}&currencies={currencyTo}&base_currency={currencyFrom}";

            using (var client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(completeUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var currencyResponse = await response.Content.ReadAsStringAsync();

                        var objResponse = (JObject)JsonConvert.DeserializeObject(currencyResponse);

                        var result = objResponse.SelectToken(@$"data.{currencyTo}.value").Value<decimal>();

                        return result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
        }
    }
}
