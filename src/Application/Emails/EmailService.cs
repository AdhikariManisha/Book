using Book.Application.Contracts.Emails;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Book.Application.Emails;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> SendAsync(EmailDto input)
    {
        try
        {
            var host = _configuration["Emails:Host"];
            var port = int.Parse(_configuration["Emails:Port"]);
            var enableSsl = bool.Parse(_configuration["Emails:EnableSsl"]);
            var username = _configuration["Emails:Username"];
            var password = _configuration["Emails:Password"];

            MailMessage mail = new();
            mail.From = new MailAddress(username);
            mail.Subject = input.Subject;
            mail.Body = input.Body;
            foreach (var toMail in input.ToEmails)
            {
                mail.To.Add(toMail);
            }

            var smtpClient = new SmtpClient(host);
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSsl;
            await smtpClient.SendMailAsync(mail);
            Console.WriteLine("Email Send Successfully");

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

