using System.ComponentModel.DataAnnotations;

namespace Book.Application.Contracts.Users;

public class UserLoginDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}