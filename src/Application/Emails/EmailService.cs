using Book.Application.Contracts.Emails;
using Book.Shared.Options;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Book.Application.Emails;
public class EmailService : IEmailService
{
    private readonly EmailOption _emailOption;

    public EmailService(IOptions<EmailOption> options)
    {
        _emailOption = options.Value;
    }
    public async Task<bool> SendAsync(EmailDto input)
    {
        try
        {
            //var host = _configuration["Email:Host"];
            //var port = int.Parse(_configuration["Email:Port"]);
            //var enableSsl = bool.Parse(_configuration["Email:EnableSsl"]);
            //var username = _configuration["Email:Username"];
            //var password = _configuration["Email:Password"];

            MailMessage mail = new();
            mail.From = new MailAddress(_emailOption.Username);
            mail.Subject = input.Subject;
            mail.Body = input.Body;
            foreach (var toMail in input.ToEmails)
            {
                mail.To.Add(toMail);
            }

            var smtpClient = new SmtpClient(_emailOption.Host);
            smtpClient.Credentials = new NetworkCredential(_emailOption.Username, _emailOption.Password);
            smtpClient.Port = _emailOption.Port;
            smtpClient.EnableSsl = _emailOption.EnableSsl;
            await smtpClient.SendMailAsync(mail);
            Console.WriteLine("Email Send Successfully");

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [AutomaticRetry(Attempts = 1)]
    public async Task<bool> SendTestEmailAsync()
    {
        try
        {
            var input = new EmailDto {
                Subject = "Test",
                Body = "Hello World!!!",
                ToEmails = new MailAddressCollection {
                    new MailAddress("manishaadhikari954@gmail.com")    
                }
            };
            await SendAsync(input);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

