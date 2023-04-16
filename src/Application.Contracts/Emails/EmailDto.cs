using System.Net.Mail;

namespace Book.Application.Contracts.Emails;

public class EmailDto
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public MailAddressCollection ToEmails { get; set; } = new();
}