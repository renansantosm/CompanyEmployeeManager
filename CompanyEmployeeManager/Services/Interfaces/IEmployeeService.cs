using CompanyEmployeeManager.DTOs.Models.Employee;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAll(int skip, int take);
    Task<EmployeeDTO> GetById(int id);
    Task<EmployeeWithPositionDTO> GetEmployeeWithPosition(int id);
    Task<EmployeeDTO> Create(CreateEmployeeDTO employeeDTO);
    Task<EmployeeDTO> Update(EmployeeDTO employeeDTO);
    Task<EmployeeDTO> Delete(int id);
}
