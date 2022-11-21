using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookGenre : BaseEntity<int>
    {
        public BookGenre()
        {
        }

        public BookGenre(int bookId, int genreId)
        {
            BookId = bookId;
            GenreId = genreId;
        }

        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}
