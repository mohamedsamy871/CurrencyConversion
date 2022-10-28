using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace SSKCurrency.Services
{
    internal class TimedConversionService : IHostedService, IDisposable
    {
        private readonly IConversionRates _rates;
        private Timer _timer;
        public IServiceScopeFactory Services { get; }

        public TimedConversionService(IServiceScopeFactory services, IConversionRates conversionRates)
        {
            Services = services;
            _rates = conversionRates;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(86400));
            return Task.CompletedTask;
        }
        public async void DoWorkAsync(object state)
        {
            await _rates.UpdateRatesInDb();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
    
}
