using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Books;
public interface IBookService
{
    Task<bool> CreateAsync(CreateUpdateBookDto dto);
    Task<bool> DeleteAsync(int id);
    Task<BookDto> GetAsync(int id);
    Task<List<BookDto>> GetListAsync();
    Task<bool> UpdateAsync(int id, CreateUpdateBookDto dto);
    Task<int> GetBookTotalCount();
}
