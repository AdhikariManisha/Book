using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Book.Authors;
public interface IAuthorRepository
{
    public Task<AuthorDto> GetAsync(int id);
    public Task<List<AuthorDto>> GetListAsync();
    public Task<int> CreateAsync(CreateUpdateAuthorDto input);
    public Task UpdateAsync(CreateUpdateAuthorDto input);
    public Task DeleteAsync(int id);
    public Task<AuthorDto> GetByNameAsync(string name);
    public Task DeleteManyAsync(List<int> ids);
}
