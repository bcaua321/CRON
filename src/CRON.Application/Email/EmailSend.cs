using CRON.Application.DTO;
using CRON.Application.Intefaces.Email;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace CRON.Application.Email;
public class EmailSend : IEmailSend
{
    private EmailConfiguration _configuration { get;}
    public EmailSend(IOptions<EmailConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task SendEmail(string subject, string body)
    {
        var email = ConstructEmail(subject, body);

        try
        {
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_configuration.Host, _configuration.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration.User, _configuration.Pass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            Console.WriteLine("Email com erro Enviado.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro ao enviar emal" + e.Message);
        }
    }

    private MimeMessage ConstructEmail(string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration.From));
        email.To.Add(MailboxAddress.Parse(_configuration.To));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{body}</h1>" };

        return email;
    }
}