﻿using CompanyEmployeeManager.DTOs.Models.Company;

namespace CompanyEmployeeManager.DTOs.Models.Address;

public class AddressWithCompaniesDTO
{
    public int AddressId { get; set; }
    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public required string PostalCode { get; set; }

    public ICollection<CompanyWithoutAddressDTO>? Companies { get; set; }
}
