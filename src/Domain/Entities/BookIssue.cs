using Book.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities
{
    public class BookIssue: BaseEntity<int>
    {
        public BookIssue()
        {
        }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public DateTime IssueDate { get; set; }
        public int IssueTo { get; set; }
        [ForeignKey("IssueTo")]
        public virtual User IssueToUser { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ReturnBy { get; set; }
        [ForeignKey("ReturnBy")]
        public virtual User ReturnByUser { get; set; }

    }
}
