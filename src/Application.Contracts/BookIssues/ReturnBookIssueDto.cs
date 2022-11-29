using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.BookIssues;
public class ReturnBookIssueDto
{
    [Required]
    public int BookId { get; set; }
    [Required]
    public DateTime ReturnDate { get; set; }
    [Required]
    public int ReturnBy { get; set; }
}
