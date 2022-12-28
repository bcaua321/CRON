using CRON.Application.Email;
using CRON.Application.Intefaces.Email;
using SendGrid.Extensions.DependencyInjection;

namespace CRON.Api.IoC;
public static class EmailServiceConfig 
{
    public static void EmailSender(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSendGrid(options => options.ApiKey = configuration.GetValue<string>("SendGridApiKey"));
        services.AddScoped<IEmailSend, EmailSend>();
    }
}
