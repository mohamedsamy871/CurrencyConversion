using System.Collections.Generic;
using System.Threading.Tasks;
using static SSKCurrency.Services.UpdateConversionRates;

namespace SSKCurrency.Services
{
    public interface IConversionRates
    {
        Task<CurrencyDetails> GetRates(string symbol);
        Task<bool> UpdateRatesInDb();
    }
}
