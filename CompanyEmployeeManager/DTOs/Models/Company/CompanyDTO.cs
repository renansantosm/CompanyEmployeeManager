namespace CompanyEmployeeManager.DTOs.Models.Company;

public class CompanyDTO
{
    public int CompanyId { get; set; }

    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public int AddressId { get; set; }
}
