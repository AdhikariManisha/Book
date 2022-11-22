using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities
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
        [MaxLength(99)]
        public string? AuthorName { get; set; }
        public bool Status { get; set; }
    }
}
