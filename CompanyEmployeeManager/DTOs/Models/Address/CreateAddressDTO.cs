namespace CompanyEmployeeManager.DTOs.Models.Address;

public class CreateAddressDTO
{
    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public required string PostalCode { get; set; }
}
