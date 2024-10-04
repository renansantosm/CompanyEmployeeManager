namespace CompanyEmployeeManager.DTOs.Models.Company;

public class CreateCompanyDTO
{
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public int AddressId { get; set; }
}
