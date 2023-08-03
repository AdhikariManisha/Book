using Microsoft.AspNetCore.Identity;

namespace Book.Domain.Entities.Identity;

public class BookUser: IdentityUser<int>
{
    public BookUser()
    {
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? DOB { get; set; }
}
