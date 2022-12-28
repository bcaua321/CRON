using CRON.Application.Intefaces.Scraping;
using CRON.Data.Context;
using CRON.Domain.Entities;
using CRON.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRON.Data.Repositories;
public class RegisterOnlyProductRepository : IRegisterOnlyProductRepository
{
    private DataContextQuartz _quartzContext { get; set; }
    public RegisterOnlyProductRepository(DataContextQuartz quartzContext)
    {
        _quartzContext = quartzContext;
    }

    public async Task<Product> RegisterProduct(Product product)
    {
        await _quartzContext.Products.AddAsync(product);
        await _quartzContext.SaveChangesAsync();

        return product;
    }
    public async Task<Product> GetProductByCodeQuartz(string code)
    {
        var exist = await ExitsProductQuartz(code);

        if (!exist)
            return null;

        return await _quartzContext.Products.Where(x => x.Code == code).FirstOrDefaultAsync();
    }


    public async Task<Product> UpdateProduct(Product product)
    {
        var productEntity = await GetProductByCodeQuartz(product.Code);

        if (productEntity is null)
            return null;

        // Will modify current values in database
        UpdateEntity(productEntity, product);

        await _quartzContext.SaveChangesAsync();

        return product;
    }

    // Only For Quartz, to avoid mistakes of using the same connection
    public async Task<bool> ExitsProductQuartz(string code)
    {
        return await _quartzContext.Products.AnyAsync(p => p.Code == code);
    }

    private void UpdateEntity(Product productEntity, Product product)
    {
        productEntity.Url = product.Url;
        productEntity.BarCode = product.BarCode;
        productEntity.Brands = product.Brands;
        productEntity.Categories = product.Categories;
        productEntity.ImageUrl = product.ImageUrl;
        productEntity.ImportedTime = DateTime.UtcNow;
        productEntity.Packaging = product.Packaging;
        productEntity.ProductName = product.ProductName;
        productEntity.Quantity = product.Quantity;
    }

}