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
        

        private readonly ILogger _logger;

        public CurrencyController([NotNull] ICurrencyProviderClient currencyProviderClient, 
                                  [NotNull] ILogger logger,
                                  [NotNull] ICurrencyManager currencyManager)

        {
            _currencyProviderClient = currencyProviderClient ?? 
                                      throw new ArgumentNullException(nameof(currencyProviderClient));
            _logger = logger ?? 
                      throw new ArgumentNullException(nameof(logger));
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

                _logger.Log("Получены валюты из апи");

                var currencies = Map(rates.Currencies);

                _currencyManager.SaveList(currencies);

                _logger.Log("Валюты сохранены");

                return Ok(currencies.Count);
            }
            catch (Exception e)
            {
                _logger.Log($"Невозможно получить и сохранить курсы валют. {e}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        [Route("save")]
        public async Task<IActionResult> Save(DateTime dateOn, string code = null)
        {
            if (dateOn <= DateTime.MinValue ||
                dateOn >= DateTime.MaxValue)
                return BadRequest();

            try
            {
                //Currency currencies = _manager.Get(dateOn, code);

                return Ok(); //currencies);
            }
            catch (Exception e)
            {
                _logger.Log($"Невозможно получить валюты {e}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        private IList<Currency> Map(CurrencyDto[] ratesCurrencies)
        {
            return null;
        }
    }
}