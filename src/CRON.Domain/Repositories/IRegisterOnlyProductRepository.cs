using CRON.Domain.Entities;

namespace CRON.Domain.Repositories;
public interface IRegisterOnlyProductRepository
{
    Task<bool> ExitsProductQuartz(string code);
    Task<Product> UpdateProduct(Product product);
    Task<Product> RegisterProduct(Product product);
}
