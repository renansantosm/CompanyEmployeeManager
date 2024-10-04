namespace CompanyEmployeeManager.DTOs.Models.Employee;

public class CreateEmployeeDTO
{
    public required string Name { get; set; }
    public required int Age { get; set; }

    public int CompanyId { get; set; }
    public int PositionId { get; set; }
}
