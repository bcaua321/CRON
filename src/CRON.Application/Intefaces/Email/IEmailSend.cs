using FluentEmail.Core;

namespace CRON.Application.Intefaces.Email;
public interface IEmailSend
{
    Task SendEmail(string subject, string body);
}