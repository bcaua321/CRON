using CRON.Application.Constants;
using CRON.Application.Intefaces.Scraping;
using CRON.Domain.Entities;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;

namespace CRON.Scraping;
public class ProductBuilder : IProductBuilder
{
    private IProductUrlScraping _productUrlScraping { get; }
    private IProductInfomationScraping _productScraping { get; }
    private IConfiguration _configuration { get; }
    private string _urlToScraping { get; }
    public ProductBuilder(IProductUrlScraping productUrlScraping, IProductInfomationScraping productScraping, IConfiguration configuration)
    {
        _productUrlScraping = productUrlScraping;
        _productScraping = productScraping;
        _configuration = configuration;
        _urlToScraping = _configuration.GetSection("site").Value;
    }

    // This method will be called by Job. And returns products scraped from page
    public async Task<List<Product>> GetProducts()
    {
        var listProducts = new List<Product>();
        var web = new HtmlWeb();

        var urls = _productUrlScraping.GetProductsUrls(_urlToScraping);

        if (urls == null)
            return null;

        foreach (var url in urls)
        {
            var htmlDocument = await web.LoadFromWebAsync($"{_urlToScraping}{url}");
            var result = ScrapingProduct(htmlDocument, url);

            listProducts.Add(result);
        }

        return listProducts;
    }

    private Product ScrapingProduct(HtmlDocument htmlDocument, string url)
    {
        var product = new Product
        {
            Url = $"{_urlToScraping}{url}",
            Status = ProductStatus.Imported
        };

        _productScraping.ProductsInfoBuilder(htmlDocument, product);

        return product;
    }
}