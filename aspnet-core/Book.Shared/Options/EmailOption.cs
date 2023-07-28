namespace Book.Shared.Options;
public class EmailOption
{
    public const string Email = "Email";
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
