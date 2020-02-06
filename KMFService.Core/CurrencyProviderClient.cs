using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KMFService.Core
{
    public class CurrencyProviderClient : ICurrencyProviderClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrencyProviderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<RatesDto> Get(DateTime dateOn)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var date = dateOn.ToString("dd.mm.yyyy");
                var strUrl = $"https://nationalbank.kz/rss/get_rates.cfm?fdate={date}";
                var result = await httpClient.GetStringAsync(strUrl);

                var xRoot = new XmlRootAttribute();
                xRoot.ElementName = "rates";
                xRoot.IsNullable = true;

                var serializer = new XmlSerializer(typeof(RatesDto), xRoot); 

                var rateObjects = serializer.Deserialize(new StringReader(result));
                 
                return rateObjects as RatesDto ?? 
                       throw new InvalidOperationException("Невозможно получить данные из апи");
            }
        }
    }
}