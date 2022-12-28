using CRON.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRON.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ProductTableBuilder();
        }
    }
}
