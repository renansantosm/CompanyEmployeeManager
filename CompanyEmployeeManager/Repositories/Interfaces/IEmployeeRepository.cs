using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll(int pageNumber, int pageSize);
    Task<int> GetAllCount();
    Task<Employee?> GetById(int id);
    Task<Employee?> GetWithPosition(int id);
    Task<Employee> Create(Employee employee);
    Task<Employee> Update(Employee employee);
    Task<Employee?> Delete(int id);
}
