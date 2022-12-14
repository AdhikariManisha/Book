using Book.Application.Contracts.Services;

namespace Book.Authors;

public class AuthorDto: BaseEntityDto<int>
{
    public string AuthorName { get; set; }
    public bool Status { get; set; }
    
}