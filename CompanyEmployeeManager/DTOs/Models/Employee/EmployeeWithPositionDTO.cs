using CompanyEmployeeManager.DTOs.Models.Position;

namespace CompanyEmployeeManager.DTOs.Models.Employee;

public class EmployeeWithPositionDTO
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }

    public required PositionDTO Position { get; set; }
}
