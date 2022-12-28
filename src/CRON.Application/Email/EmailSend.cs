using CRON.Application.Intefaces.Email;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CRON.Application.Email;
public class EmailSend : IEmailSend
{
    private IConfiguration _configuration { get; }
    private ISendGridClient _client { get; }
    public EmailSend(IConfiguration configuration, ISendGridClient client)
    {
        _configuration = configuration;
        _client = client;
    }

    public async Task SendEmail(string subject, string body)
    {
        var fromEmail = _configuration.GetSection("from").Value;
        var fromName = _configuration.GetSection("fromName").Value;
        var toEmail = _configuration.GetSection("to").Value;

        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = subject,
            PlainTextContent = body
        };

        msg.AddTo(new EmailAddress(toEmail));

        try
        {
            var response = await _client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}