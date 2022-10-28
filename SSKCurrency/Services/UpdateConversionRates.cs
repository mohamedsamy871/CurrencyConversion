using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using SSKCurrency.Data;
using Microsoft.CodeAnalysis;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace SSKCurrency.Services
{
    public class UpdateConversionRates:IConversionRates
    {
        private readonly DataContext _db;
        private readonly AppConfiguration _appConfiguration;
        public UpdateConversionRates(DataContext db, IOptions<AppConfiguration> appConfiguration)
        {
            _db = db;
            _appConfiguration = appConfiguration.Value; 
        }
        public async Task<CurrencyDetails> GetRates(string symbol)
        {
            var appKey = "7ZD-OHjwnDT4WcYCM9E5";// _appConfiguration.AppKey;
            var url = "https://marketdata.tradermade.com";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/api/v1/live?currency={symbol}&api_key={appKey}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CurrencyDetails>(responseBody);
            return result;
        }
        public async Task<bool> UpdateRatesInDb()
        {
            var symbolsList = _db.Conversion.Where(m=>m.AllowSync==true).Select(m=>m.Symbol).ToList();
            string _multiSymbolString = String.Join(",", symbolsList);
            var apiResult = await GetRates(_multiSymbolString);
            foreach (var item in symbolsList)
            {
                var conversionItemFromDb = _db.Conversion.Where(m => m.Symbol == item).FirstOrDefault();
                foreach (var updatedRate in apiResult.quotes)
                {
                    if(updatedRate.base_currency == item.Substring(0,3)&& updatedRate.quote_currency == item.Substring(3,3))
                        conversionItemFromDb.ExRate = updatedRate.mid.ToString();
                }
                conversionItemFromDb.LastSync = apiResult.requested_time;
                _db.SaveChanges();
            }
            return true;
        }
        public class CurrencyDetails
        {
            public string endpoint { get; set; }
            public quotes[] quotes { get; set; }
            public string requested_time { get; set; }
            public long timestamp { get; set; }

        }
        public class quotes
        {
            public double ask { get; set; }
            public double bid { get; set; }
            public string base_currency { get; set; }
            public double mid { get; set; }
            public string quote_currency { get; set; }
        }
    }
}
