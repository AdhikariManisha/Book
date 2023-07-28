namespace Book.Authors;

public class AuthorFilter
{
    public string? AuthorName { get; set; }
    public bool? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}