using CRON.Application.DTO.Response;
using CRON.Domain.Entities;

namespace CRON.Services.Services.ProductServices;
public interface IReadOnlyProductService
{
    public Task<Product> GetProductByCode(string code);
    public Task<ProductsPaginationFilter> GetProducts(int page, int pageSize);
}