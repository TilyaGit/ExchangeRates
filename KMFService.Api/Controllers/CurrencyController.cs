using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using KMFService.Core;
using Microsoft.AspNetCore.Mvc;

namespace KMFService.Api.Controllers
{
    [Route("api/currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyProviderClient _currencyProviderClient;
        private readonly ICurrencyManager _currencyManager;

        public CurrencyController([NotNull] ICurrencyProviderClient currencyProviderClient,
                                  [NotNull] ICurrencyManager currencyManager)
        {
            _currencyProviderClient = currencyProviderClient ?? 
                                      throw new ArgumentNullException(nameof(currencyProviderClient));
            _currencyManager = currencyManager ?? 
                               throw new ArgumentNullException(nameof(currencyManager));
        }

        [HttpGet]
        [Route("save")]
        public async Task<IActionResult> Save(DateTime dateOn)
        {
            if (dateOn <= DateTime.MinValue ||
                dateOn >= DateTime.MaxValue)
                return BadRequest();

            try
            { 
                var rates = await _currencyProviderClient.Get(dateOn);

                var currencies = Map(rates, dateOn);

                _currencyManager.SaveList(currencies);

                return Ok(currencies.Count);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        [Route("currency")]
        public async Task<IActionResult> GetCurrency()//DateTime dateOn, string code = null
        {
            DateTime dateOn = DateTime.Today;
            string code = null;

            if (dateOn <= DateTime.MinValue ||
                dateOn >= DateTime.MaxValue)
                return BadRequest();

            try
            {
                var currencies = _currencyManager.GetList(dateOn, code);

                return Ok(currencies); 
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        public IList<Currency> Map(RatesDto ratesDto, DateTime date)
        {
            var currencies = new List<Currency>();

            foreach (var currencyDto in ratesDto.Currencies)
            {
                var currency = new Currency
                {
                    Title = currencyDto.Title,
                    Code = currencyDto.Quant,
                    Value = currencyDto.Description,
                    Date = date,
                };

                currencies.Add(currency);
            }

            return currencies;
        }
    }
}