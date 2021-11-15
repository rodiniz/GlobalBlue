using Microsoft.EntityFrameworkCore;

namespace GlobalBlue.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}