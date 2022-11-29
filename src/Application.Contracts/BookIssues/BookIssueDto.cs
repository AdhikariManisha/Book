using Book.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.BookIssues;
public class BookIssueDto: BaseEntityDto<int>
{
    public int BookId { get; set; }
    public DateTime IssueDate { get; set; }
    public int IssueTo { get; set; }
    public string? IssueToName { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int? ReturnBy { get; set; }
    public string? ReturnByName { get; set; }
    public string? BookName { get; set; }
}
