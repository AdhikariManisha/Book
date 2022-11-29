using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.BookIssues;
public class CreateUpdateBookIssueDto
{
    [Required]
    public int BookId { get; set; }
    [Required]
    public DateTime IssueDate { get; set; }
    [Required]
    public int IssueTo { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int? ReturnBy { get; set; }
}
