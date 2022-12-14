using Book.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Books;
public class BookAuthorDto: BaseEntityDto<int>
{
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
}
