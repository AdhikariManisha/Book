using System.ComponentModel.DataAnnotations;

namespace Book.Application.Contracts.Users;

public class UserRegisterDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public DateTime? DOB { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
    public  string PhoneNumber { get; set; }
}