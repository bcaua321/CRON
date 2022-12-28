using CRON.Api.Extensions;
using CRON.Api.Filters;
using CRON.Api.IoC;

namespace CRON.Api;
public class Startup : Interfaces.IStartup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // Database Context
        services.AddDataAcess(Configuration);

        // Email Sender
        services.EmailSender(Configuration);

        // Product Scraping
        services.UseProductScraping();
        services.UseProductScrapingSheduler(Configuration);

        // Services 
        services.AddServices();

        // Api Configurations
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers();
        services.AddVersioning();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMvc(x => x.Filters.Add(typeof(ExceptionFilters)));
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();
    }
}