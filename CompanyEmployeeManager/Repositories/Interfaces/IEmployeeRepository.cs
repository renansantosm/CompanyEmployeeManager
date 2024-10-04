using CompanyEmployeeManager.Models;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll(int skip, int take);
    Task<Employee?> GetById(int id);
    Task<Employee> Create(Employee employee);
    Task<Employee> Update(Employee employee);
    Task<Employee> Delete(int id);
}
