using System.ComponentModel.DataAnnotations;

namespace SSKCurrency.Data
{
    public class Conversion
    {
        public int Id { get; set; }
        [Required]
        public string Symbol { get; set; }
        public string ExRate { get; set; }
        public bool AllowSync { get; set; }
        public string LastSync { get; set; }
    }
}
