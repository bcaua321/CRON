using CRON.Scraping.Jobs;
using Quartz;

namespace CRON.Api.Extensions;
public static class QuartzSheduleSetup
{
    public static void UseProductScrapingSheduler(this IServiceCollection services, IConfiguration configuration)
    {
        var time = configuration.GetSection("CronShedule").Value;

        // Scraping Automation
        services.AddQuartz(q =>
        {
            // Provide DI to jobs
            q.UseMicrosoftDependencyInjectionJobFactory();

            // Take a cron expression from appsettings.json and execute ProductJob once a day
            /*
            q.ScheduleJob<ProductJob>(trigger => trigger
                .WithIdentity("Product Scraping.")
                .WithCronSchedule(time)
            ); 
            */

            //  Or Execute job immediately
            q.ScheduleJob<ProductJob>(trigger => trigger
                .WithIdentity("Product Scraping.")
            );
        });

        // For respect lifecycle of Application
        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
    }
}