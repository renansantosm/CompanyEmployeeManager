using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.DTOs.Models.Pagination;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface ICompanyService
{
    Task<PagedResultListDTO<CompanyDTO>> GetAll(int pageNumber, int pageSize);
    Task<CompanyDTO?> GetById(int id);
    Task<CompanyWithAddressDTO?> GetCompanyByIdWithAddress(int id);
    Task<PagedResultDto<CompanyWithEmployeesPagedDTO>?> GetCompanyByIdWithEmployeesPaged(int id,int pageNumber, int pageSize);
    Task<CompanyDTO> Create(CreateCompanyDTO companyDTO);
    Task<CompanyDTO> Update(CompanyDTO companyDTO);
    Task<CompanyDTO?> Delete(int id);
}
