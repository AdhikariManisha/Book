namespace Book.Application.Contracts.Emails;
public interface IEmailService
{
    Task<bool> SendAsync(EmailDto input);
}
