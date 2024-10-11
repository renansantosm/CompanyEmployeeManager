using CompanyEmployeeManager.DTOs.Models.Company;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDTO>> GetAll(int skip, int take);
    Task<CompanyDTO?> GetById(int id);
    Task<CompanyWithAddressDTO?> GetCompanyAddress(int id);
    Task<CompanyWithEmployeesDTO?> GetCompanyEmployees(int id);
    Task<CompanyDTO> Create(CreateCompanyDTO companyDTO);
    Task<CompanyDTO> Update(CompanyDTO companyDTO);
    Task<CompanyDTO> Delete(int id);
}
