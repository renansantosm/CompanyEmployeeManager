﻿using CompanyEmployeeManager.DTOs.Models.Employee;

namespace CompanyEmployeeManager.DTOs.Models.Company;

public class CompanyWithEmployeesPagedDTO
{
    public int CompanyId { get; set; }

    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public IEnumerable<EmployeeWithoutCompanyDTO> Employees { get; set; }
}
