using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface ICompanyService
{
    Task<PagedList<CompanyDTO>> GetAll(int pageNumber, int pageSize);
    Task<CompanyDTO?> GetById(int id);
    Task<CompanyWithAddressDTO?> GetCompanyAddress(int id);
    Task<CompanyWithEmployeesDTO?> GetCompanyEmployees(int id);
    Task<CompanyDTO> Create(CreateCompanyDTO companyDTO);
    Task<CompanyDTO> Update(CompanyDTO companyDTO);
    Task<CompanyDTO> Delete(int id);
}
