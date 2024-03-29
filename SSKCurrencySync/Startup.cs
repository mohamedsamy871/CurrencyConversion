using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSKCurrencySync.Data;
using SSKCurrencySync.Services;

namespace SSKCurrencySyncSync
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
            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("CurrencyConnection")));
            services.AddScoped<UpdateConversionRates>();
            services.AddHostedService<TimedConversionService>();
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            //app.UseAuthorization();
        }
    }
}
