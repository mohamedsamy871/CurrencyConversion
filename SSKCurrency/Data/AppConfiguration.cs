using System;
using System.Collections.Generic;
using System.Text;

namespace SSKCurrency.Data
{
    public class AppConfiguration
    {
        public string CurrencyConnectionString { get; set; }
        public string AppKey { get; set; }
        public int TimerInterval { get; set; }
    }
}
