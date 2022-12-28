using CRON.Application.DTO.Response;
using CRON.Domain.Entities;
using CRON.Domain.Repositories;

namespace CRON.Services.Services.ProductServices;
public class ProductService : IReadOnlyProductService
{
    private IReadOnlyProductRepository _productRepository { get; set; }
    public ProductService(IReadOnlyProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetProductByCode(string code)
    {
        var product = await _productRepository.GetProductByCode(code);

        if(product is null)
            return null;


        return product;
    }

    public async Task<ProductsPaginationFilter> GetProducts(int page, int pageSize)
    {
        var totalProducts = await _productRepository.TotalProducts();
        var pagination = new ProductsPaginationFilter(totalProducts, page, pageSize);

        if (pagination.CurrentPage > pagination.TotalPages + 1 || pagination.CurrentPage == 1)
        {
            pagination.HasPrevious = false;
        }

        if (pagination.CurrentPage < pagination.TotalPages)
        {
            pagination.HasNext = true;
        }

        var products = await _productRepository.GetProducts(pagination.Page, pagination.PageSize);

        if (!products.Any())
        {
            pagination.AddError($"Page does not exist");
            return pagination;
        }

        pagination.Products = products;
        return pagination;
    }

}