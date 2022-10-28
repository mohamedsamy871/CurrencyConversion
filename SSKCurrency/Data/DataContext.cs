using Microsoft.EntityFrameworkCore;

namespace SSKCurrency.Data
{
    public class DataContext:DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Conversion> Conversion { get; set; }
    }
}
