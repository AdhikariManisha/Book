using System.ComponentModel.DataAnnotations;

namespace Book.Application.Contracts.UserAccounts;

public class UserLoginDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; } 
}