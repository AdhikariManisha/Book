namespace Book.Shared.Dtos;
public class PagedResultRequestDto
{
    public int SkipCount { get; set; }
    public int TakeCount { get; set; } = 10;
}
