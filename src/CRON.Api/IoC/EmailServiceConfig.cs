using CRON.Application.DTO;
using CRON.Application.Email;
using CRON.Application.Intefaces.Email;

namespace CRON.Api.IoC;
public static class EmailServiceConfig 
{
    public static void EmailSender(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfiguration>(configuration.GetSection(nameof(EmailConfiguration)));
        services.AddScoped<IEmailSend, EmailSend>();
    }
}
