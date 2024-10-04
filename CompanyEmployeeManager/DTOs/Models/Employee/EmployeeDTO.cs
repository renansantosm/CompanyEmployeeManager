namespace CompanyEmployeeManager.DTOs.Models.Employee;

public class EmployeeDTO
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }

    public int? CompanyId { get; set; }
    public int? PositionId { get; set; }
}
