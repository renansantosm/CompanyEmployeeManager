using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAll(int pageNumber, int pageSize);
    Task<int> GetAllCount();
    Task<Company?> GetCompanyByIdWithAddress(int id);
    Task<Company?> GetCompanyByIdWithEmployeesPaged(int id, int pageNumber, int pageSize);
    Task<int> GetCompanyByIdWithEmployeesCount(int id);
    Task<Company?> GetById(int id);
    Task<Company> Create(Company company);
    Task<Company> Update(Company company);
    Task<Company?> Delete(int id);
}
