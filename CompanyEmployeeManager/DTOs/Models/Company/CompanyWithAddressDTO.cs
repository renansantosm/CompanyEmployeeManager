using CompanyEmployeeManager.DTOs.Models.Addresses;

namespace CompanyEmployeeManager.DTOs.Models.Company;

public class CompanyWithAddressDTO
{
    public int CompanyId { get; set; }

    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required AddressDTO Address { get; set; }
}
