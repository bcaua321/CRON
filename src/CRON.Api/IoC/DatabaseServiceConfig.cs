using CRON.Data.Context;
using CRON.Data.Repositories;
using CRON.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRON.Api.IoC;
public static class DatabaseServiceConfig
{
    public static void AddDataAcess(this IServiceCollection services, IConfiguration configuration)
    {
        var version = new MySqlServerVersion(new Version(8, 0, 29));
        var connectionString = configuration.GetConnectionString($"CronConnection");

        services.AddDbContext<DataContext>(x =>
                x.UseMySql(connectionString, version, mySqlOptions =>
                {
                    mySqlOptions.MigrationsAssembly(typeof(DataContext).Assembly.FullName); // because it's in other reference
                }));

        // Other context only for quartz
        services.AddDbContext<DataContextQuartz>(x => x.UseMySql(connectionString, version)); 

        services.AddScoped<IReadOnlyProductRepository, ReadOnlyProductRepository>();
        services.AddScoped<IRegisterOnlyProductRepository, RegisterOnlyProductRepository>();
    }
}