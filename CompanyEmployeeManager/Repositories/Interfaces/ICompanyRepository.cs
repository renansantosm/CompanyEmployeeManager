using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task<PagedList<Company>> GetAll(int pageNumber, int pageSize);
    Task<Company?> GetWithAddress(int id);
    Task<Company?> GetWithEmployees(int id);
    Task<Company?> GetById(int id);
    Task<Company?> Create(Company company);
    Task<Company> Update(Company company);
    Task<Company> Delete(int id);
}
