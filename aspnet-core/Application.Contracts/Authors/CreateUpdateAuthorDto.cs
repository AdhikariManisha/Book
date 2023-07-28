namespace Book.Authors;

public class CreateUpdateAuthorDto
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public bool Status { get; set; }
}