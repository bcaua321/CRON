using CRON.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRON.Data.Context;
public static class ProductTable
{
    public static void ProductTableBuilder(this ModelBuilder builder)
    {
        var productTable = builder.Entity<Product>();

        productTable.HasIndex(x => x.Code);
    }
}