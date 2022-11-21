using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookAuthor : BaseEntity<int>
    {
        public BookAuthor()
        {
        }

        public BookAuthor(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }

        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
