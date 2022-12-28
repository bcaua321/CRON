using CRON.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRON.Data.Context;
public class DataContextQuartz : DbContext
{
    public DataContextQuartz(DbContextOptions<DataContextQuartz> options) : base(options) { }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ProductTableBuilder();
    }
}
