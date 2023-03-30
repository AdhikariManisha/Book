using Book.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Books
{
    public class BookDto : BaseEntityDto<int>
    {
        public string BookName { get; set; }
        public int? BookIssueId { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? IssueTo { get; set; }
        public string? IssueToName { get; set; }
        public int? IssueBy { get; set; }
        public string? IssueByName { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ReturnBy { get; set; }
        public string? ReturnByName { get; set; }
        public bool IsInLibrary { get; set; }
        public List<BookAuthorDto> Authors { get; set; } = new();
        public List<BookGenreDto> Genres { get; set; } = new();
    }
}