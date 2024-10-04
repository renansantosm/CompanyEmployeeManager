using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll(int skip = 0, int take = 10)
    {
        var employeesDto = await _service.GetAll(skip, take);

        if (employeesDto is null)
            return NotFound();

        return Ok(employeesDto);
    }

    [HttpGet("{id:int}", Name = "GetEmployee")]
    public async Task<ActionResult<EmployeeDTO>> Get(int id)
    {
        var employeeDto = await _service.GetById(id);

        if (employeeDto is null)
            return NotFound();

        return Ok(employeeDto);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDTO>> Create(CreateEmployeeDTO employeeDTO)
    {
        if (employeeDTO is null)
            return BadRequest();

        var createdEmployeeDto = await _service.Create(employeeDTO);

        return new CreatedAtRouteResult("GetEmployee",
            new { id = createdEmployeeDto.EmployeeId }, createdEmployeeDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmployeeDTO>> Update(int id, EmployeeDTO employeeDTO)
    {
        if (employeeDTO.EmployeeId != id)
            return BadRequest();

        if (employeeDTO is null)
            return BadRequest();

        var updatedEmployeeDto = await _service.Update(employeeDTO);

        return Ok(updatedEmployeeDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<EmployeeDTO>> Delete(int id)
    {
        var employeeDto = await _service.GetById(id);

        if (employeeDto is null)
            return NotFound();

        var deletedEmployeeDto = await _service.Delete(id);

        return Ok(deletedEmployeeDto);
    }

}
