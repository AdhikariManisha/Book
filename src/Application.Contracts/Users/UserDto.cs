using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Users;
public class UserDto 
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? DOB { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
