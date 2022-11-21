using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book: BaseEntity<int>
    {
        public Book()
        {
        }
        public Book(string? bookName, int? bookIssueId, DateTime? issueDate, int? issueTo, int? issueBy, DateTime? returnDate, int? returnBy, bool isInLibrary)
        {
            BookName = bookName;
            BookIssueId = bookIssueId;
            IssueDate = issueDate;
            IssueTo = issueTo;
            IssueBy = issueBy;
            ReturnDate = returnDate;
            ReturnBy = returnBy;
            IsInLibrary = isInLibrary;
        }

        public string? BookName { get; set; }
        public int? BookIssueId { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? IssueTo { get; set; }
        public int? IssueBy { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ReturnBy { get; set; }
        public bool IsInLibrary { get; set; }
    }
}
