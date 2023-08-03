using System.ComponentModel.DataAnnotations;

namespace Book.Application.Contracts.Books;
public class CreateUpdateBookDto
{
    [Required]
    [StringLength(100)]
    public string BookName { get; set; }
    public List<int> Authors { get; set; } = new();
    public List<int> Genres { get; set; } = new();
}
