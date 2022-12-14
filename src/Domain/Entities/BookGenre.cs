using Book.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities
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
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public int GenreId { get; set; }
    }
}
