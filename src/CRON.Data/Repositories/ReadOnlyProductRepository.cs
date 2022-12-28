using CRON.Data.Context;
using CRON.Domain.Entities;
using CRON.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRON.Data.Repositories
{
    public class ReadOnlyProductRepository : IReadOnlyProductRepository
    {
        private DataContext _context { get; set; }

        public ReadOnlyProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> TotalProducts()
        {
            return await _context.Products.AsNoTracking().CountAsync();
        }

        public async Task<Product> GetProductByCode(string code)
        {
            var exist = await ExitsProduct(code);

            if (!exist)
                return null;

            return await _context.Products.AsNoTracking().Where(x => x.Code == code).FirstOrDefaultAsync();
        }

        public async Task<bool> ExitsProduct(string code)
        {
            return await _context.Products.AsNoTracking().AnyAsync(x => x.Code == code);
        }

        public async Task<List<Product>> GetProducts(int page, int pageSize)
        {
            var result = await _context.Products
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }
    }
}
