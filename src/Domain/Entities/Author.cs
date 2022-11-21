using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author : BaseEntity<int>
    {
        public Author()
        {
        }

        public Author(string? authorName, bool status)
        {
            AuthorName = authorName;
            Status = status;
        }

        public string? AuthorName { get; set; }
        public bool Status { get; set; }
    }
}
