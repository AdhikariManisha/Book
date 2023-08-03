using Book.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.Domain.Entities
{
    [Table("Book")]
    public class Book: BaseEntity<int>
    {
        public Book()
        {
        }
        public Book(string bookName, int? bookIssueId, DateTime? issueDate, int? issueTo, int? issueBy, DateTime? returnDate, int? returnBy, bool isInLibrary)
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
        [Column("Name", Order = 4, TypeName = "varchar")]
        [StringLength(100)]
        public string BookName { get; set; }
        public int? BookIssueId { get; set; }
        [Column("IssuedDate", Order = 0, TypeName = "datetime2")]
        public DateTime? IssueDate { get; set; }
        public int? IssueTo { get; set; }
        public int? IssueBy { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ReturnBy { get; set; }
        public bool IsInLibrary { get; set; }
    }
}
