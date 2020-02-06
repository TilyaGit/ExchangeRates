using System.Net.Http;
using System.Threading.Tasks;
using KMFService.Core;
using Newtonsoft.Json;

namespace KMFService.Api
{
    public class CurrencyProviderClient : ICurrencyProviderClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrencyProviderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Rates> Get()
        {
            var httpСFactory = _httpClientFactory;

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var strUrl = "https://nationalbank.kz/rss/get_rates.cfm?fdate=04.02.2020";
                var result = await httpClient.GetStringAsync(strUrl);
                return JsonConvert.DeserializeObject<Rates>(result);
            }
        }
    }
}
