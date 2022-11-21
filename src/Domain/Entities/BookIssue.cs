using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookIssue: BaseEntity<int>
    {
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public int IssueTo { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ReturnBy { get; set; }
    }
}
