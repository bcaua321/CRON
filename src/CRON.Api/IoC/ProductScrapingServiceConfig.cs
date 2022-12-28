using CRON.Application.Intefaces.Scraping;
using CRON.Scraping;
using CRON.Scraping.ProductScraping;
using Microsoft.Extensions.DependencyInjection;

namespace CRON.Api.IoC;
public static class ProductScrapingServiceConfig
{
    public static void UseProductScraping(this IServiceCollection services)
    {
        services.AddScoped<IProductBuilder, ProductBuilder>();
        services.AddScoped<IProductInfomationScraping, ProductInfomartionScraping>();
        services.AddScoped<IProductUrlScraping, ProductUrlScraping>();
    }
}