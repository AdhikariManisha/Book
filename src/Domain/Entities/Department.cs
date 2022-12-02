using Book.Shared.Entities;

namespace Book.Domain.Entities;
public class Department: BaseEntity<int>
{
    public Department()
    {
    }

    public string DepartmentName { get; set; }
    public string? Faculty { get; set; }
 }
