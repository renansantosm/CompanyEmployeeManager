namespace CompanyEmployeeManager.DTOs.Models.Company;

public class CompanyWithoutAddressDTO
{
    public int CompanyId { get; set; }

    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
}
