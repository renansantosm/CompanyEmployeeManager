using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IEmployeeService
{
    Task<PagedList<EmployeeDTO>> GetAll(int pageNumber, int pageSize);
    Task<EmployeeDTO> GetById(int id);
    Task<EmployeeWithPositionDTO> GetEmployeeWithPosition(int id);
    Task<EmployeeDTO> Create(CreateEmployeeDTO employeeDTO);
    Task<EmployeeDTO> Update(EmployeeDTO employeeDTO);
    Task<EmployeeDTO> Delete(int id);
}
