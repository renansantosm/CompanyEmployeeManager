using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Employee;

namespace CompanyEmployeeManager.DTOs.Models.Company;

public class CompanyWithEmployeesDTO
{
    public int CompanyId { get; set; }

    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public ICollection<EmployeeDTO> Employees { get; set; }
}
