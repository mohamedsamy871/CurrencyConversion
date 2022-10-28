namespace SSKCurrencySync.Data
{
    public class CurrencyDetails
    {
        public string endpoint { get; set; }
        public quotes[] quotes { get; set; }
        public string requested_time { get; set; }
        public long timestamp { get; set; }
    }
}
