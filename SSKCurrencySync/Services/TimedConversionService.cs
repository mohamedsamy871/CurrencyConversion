using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SSKCurrencySync.Data;

namespace SSKCurrencySync.Services
{
    internal class TimedConversionService : IHostedService, IDisposable
    {
        private Timer _timer;
        public IServiceProvider Services { get; }
        private readonly AppConfiguration _appConfiguration;
        public TimedConversionService(IServiceProvider services, IOptions<AppConfiguration> appConfiguration)
        {
            Services = services;
            _appConfiguration = appConfiguration.Value;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero,
                 TimeSpan.FromSeconds(_appConfiguration.TimerIntervalInHour*3600));
            return Task.CompletedTask;
        }
        public async void DoWorkAsync(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<UpdateConversionRates>();
                await scopedProcessingService.UpdateRatesInDb();
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
           // _timer?.Dispose();
        }
    }
    
}
