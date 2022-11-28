using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Books;
public class CreateUpdateBookDto
{
    [Required]
    [StringLength(100)]
    public string BookName { get; set; }
    public List<int> Authors { get; set; } = new();
    public List<int> Genres { get; set; } = new();
}
