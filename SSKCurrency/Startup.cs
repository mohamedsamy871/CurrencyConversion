using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SSKCurrency.Data;
using SSKCurrency.Services;

namespace SSKCurrency
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer("Server=.;Database=SSKCurrency;Trusted_Connection=True;MultipleActiveResultSets=true"));
            //Configuration.GetConnectionString("CurrencyConnectionString")
            services.AddSingleton<IConversionRates, UpdateConversionRates>();
            services.AddHostedService<TimedConversionService>();
            //services.AddSingleton<ITimedConversionService, TimedConversionService>();
        }
        public void Configure(IApplicationBuilder app)
        {
        }

    }
}