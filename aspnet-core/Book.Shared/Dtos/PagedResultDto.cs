using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.Dtos;
public class PagedResultDto<T>
{
    public PagedResultDto(long totalCount, IReadOnlyList<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }

    public long TotalCount { get; set; }
    public IReadOnlyList<T> Items { get; set; } = new List<T>();
}
