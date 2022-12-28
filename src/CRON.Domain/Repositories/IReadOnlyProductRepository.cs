using CRON.Domain.Entities;

namespace CRON.Domain.Repositories;
public interface IReadOnlyProductRepository
{
    public Task<int> TotalProducts();
    Task<bool> ExitsProduct(string code);
    public Task<Product> GetProductByCode(string code);
    public Task<List<Product>> GetProducts(int page, int pageSize);
}