using CRON.Services.Services.ProductServices;

namespace CRON.Api.IoC;
public static class ProductReadServiceConfig
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyProductService, ProductService>();
    }
}