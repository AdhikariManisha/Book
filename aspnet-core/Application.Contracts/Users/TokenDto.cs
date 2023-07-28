namespace Book.Application.Contracts.Users;

public class TokenDto
{
    public TokenDto(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }

    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}