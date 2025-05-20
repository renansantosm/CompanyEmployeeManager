namespace CompanyEmployeeManager.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public int PositionId { get; set; }
    public Position? Position { get; set; }

}
