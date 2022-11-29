using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Services;
public interface ICrudService<TId, TCreateUpdateDto, TDto>
{
    public Task<TDto> GetAsync(TId id);
    public Task<List<TDto>> GetListAsync();
    public Task<bool> CreateAsync(TCreateUpdateDto dto);
    public Task<bool> UpdateAsync(TId id, TCreateUpdateDto dto);
    public Task<bool> DeleteAsync(TId id);
}
