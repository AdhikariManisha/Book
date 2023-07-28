using Book.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Books;
public class BookGenreDto: BaseEntityDto<int>
{
    public int GenreId { get; set; }
    public string GenreName { get; set; }
}
