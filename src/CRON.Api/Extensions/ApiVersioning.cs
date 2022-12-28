using Microsoft.AspNetCore.Mvc;

namespace CRON.Api.Extensions;
public static class ApiVersioning
{
    public static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
             setup.ReportApiVersions = true;
             setup.DefaultApiVersion = ApiVersion.Default; //new ApiVersion(1, 0);
             setup.AssumeDefaultVersionWhenUnspecified = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}