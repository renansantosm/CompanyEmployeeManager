using CompanyEmployeeManager.DTOs.Models.Employee;

namespace CompanyEmployeeManager.DTOs.Models.Position;

public class PositionWithEmployeesDTO
{
    public int PositionId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public IEnumerable<EmployeeDTO>? Employees { get; set; }
}
