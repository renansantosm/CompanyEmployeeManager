using System.Collections.ObjectModel;

namespace CompanyEmployeeManager.Models;

public class Position
{
    public Position()
    {
        Employees = new Collection<Employee>();
    }

    public int PositionId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Employee>? Employees { get; set; }
}
