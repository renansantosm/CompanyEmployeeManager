using CompanyEmployeeManager.Models;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAll(int skip, int take);
    Task<Company?> GetWithAddress(int id);
    Task<Company?> GetWithEmployees(int id);
    Task<Company?> GetById(int id);
    Task<Company?> Create(Company company);
    Task<Company> Update(Company company);
    Task<Company> Delete(int id);
}
