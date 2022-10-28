using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSKCurrency.Services
{
    public interface ITimedConversionService:IHostedService, IDisposable
    {
        void DoWorkAsync(object state);

    }
}
