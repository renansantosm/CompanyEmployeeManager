using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Repositories.Interfaces;
using CompanyEmployeeManager.Services.Interfaces;

namespace CompanyEmployeeManager.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IMapper mapper, IEmployeeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PagedList<EmployeeDTO>> GetAll(int pageNumber, int pageSize)
    {
        var employees = await _repository.GetAll(pageNumber, pageSize);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

        return new PagedList<EmployeeDTO>(employeesDto, pageNumber, pageSize, employees.TotalCount);
    }

    public async Task<EmployeeDTO> GetById(int id)
    {
        var employee = await _repository.GetById(id);
        return _mapper.Map<EmployeeDTO>(employee);
    }
    public async Task<EmployeeWithPositionDTO> GetEmployeeWithPosition(int id)
    {
        var employee = await _repository.GetWithPosition(id);
        return _mapper.Map<EmployeeWithPositionDTO>(employee);
    }

    public async Task<EmployeeDTO> Create(CreateEmployeeDTO employeeDTO)
    {
        var employee = _mapper.Map<Employee>(employeeDTO);
        var createdEmployee = await _repository.Create(employee);
        return _mapper.Map<EmployeeDTO>(createdEmployee);
    }

    public async Task<EmployeeDTO> Update(EmployeeDTO employeeDTO)
    {
        var employee = _mapper.Map<Employee>(employeeDTO);
        var updatedEmployee = await _repository.Update(employee);
        return _mapper.Map<EmployeeDTO>(updatedEmployee);
    }

    public async Task<EmployeeDTO> Delete(int id)
    {
        var deletedEmployee = await _repository.Delete(id);
        return _mapper.Map<EmployeeDTO>(deletedEmployee);
    }
}
