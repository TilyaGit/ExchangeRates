using System;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace KMFService.Core
{
    public interface ICurrencyProviderClient
    {
        [ItemNotNull]
        Task<RatesDto> Get(DateTime dateOn);
    }
}
