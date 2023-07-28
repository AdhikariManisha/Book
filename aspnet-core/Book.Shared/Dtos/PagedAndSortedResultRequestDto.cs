namespace Book.Shared.Dtos;
public class PagedAndSortedResultRequestDto: PagedResultRequestDto
{
    public string? Sorting { get; set; }
}
