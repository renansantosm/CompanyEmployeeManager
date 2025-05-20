using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retrieves a paginated list of employees.
    /// </summary>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, including <c>PageNumber</c> (the page number) and <c>PageSize</c> (the number of items per page).
    /// </param>
    /// <returns>
    /// A paginated list of employees wrapped in an <c>ActionResult</c>.
    /// If successful, returns an <c>Ok</c> (200) response with the paginated data; otherwise, returns an empty list.
    /// </returns>    
    [HttpGet]
    public async Task<ActionResult<PagedResultListDTO<EmployeeDTO>>> GetAll([FromQuery] PaginationParameters paginationParams)
    {
        var employeesPagedDto = await _service.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

        return Ok(employeesPagedDto);
    }

    /// <summary>
    /// Retrieves an employee by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the employee to be fetched.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>EmployeeDTO</c>.
    /// Returns <c>Ok</c> (200) with the employee data, or <c>NotFound</c> (404) if the employee is not found.
    /// </returns>
    [HttpGet("{id:int}", Name = "GetEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeDTO>> Get(int id)
    {
        var employeeDto = await _service.GetById(id);

        if (employeeDto is null)
            return NotFound($"The employee with ID: {id} was not found.");

        return Ok(employeeDto);
    }

    /// <summary>
    /// Retrieves a employee by its ID, including its position information.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the employee to be retrieved.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing a <c>EmployeeWithPositionDTO</c> object.
    /// Returns <c>Ok</c> (200) with the employee and position details, or <c>NotFound</c> (404) if the employee is not found.
    /// </returns>    
    [HttpGet("{id:int}/position", Name = "GetEmployeeWithPosition")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeWithPositionDTO>> GetEmployeeWithPosition(int id)
    {
        var employeeWithPositionDto = await _service.GetEmployeeWithPosition(id);

        if (employeeWithPositionDto is null)
            return NotFound($"The employee with ID: {id} was not found.");

        return Ok(employeeWithPositionDto);
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employeeDTO">
    /// The <c>CreateEmployeeDTO</c> object containing the employee details to be created.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the created <c>EmployeeDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the input data is null, and <c>CreatedAtRoute</c> (201) if the employee is successfully created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeDTO>> Create(CreateEmployeeDTO employeeDTO)
    {
        if (employeeDTO is null)
            return BadRequest("Employee data is required and cannot be empty.");

        var createdEmployeeDto = await _service.Create(employeeDTO);

        return new CreatedAtRouteResult("GetEmployee",
            new { id = createdEmployeeDto.EmployeeId }, createdEmployeeDto);
    }

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the employee to be updated.
    /// </param>
    /// <param name="employeeDTO">
    /// The <c>EmployeeDTO</c> object containing updated employee details.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the updated <c>EmployeeDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the provided ID does not match or if the input data is null, and <c>Ok</c> (200) if the update is successful.
    /// </returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeDTO>> Update(int id, EmployeeDTO employeeDTO)
    {
        if (employeeDTO.EmployeeId != id)
            return BadRequest("The provided employee ID does not match the ID in the request.");

        if (employeeDTO is null)
            return BadRequest("Employee data cannot be empty. Please provide the necessary information for the update.");

        var updatedEmployeeDto = await _service.Update(employeeDTO);

        return Ok(updatedEmployeeDto);
    }

    /// <summary>
    /// Deletes an employee by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the employee to be deleted.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the deleted <c>EmployeeDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the employee is not found, and <c>Ok</c> (200) if the deletion is successful.
    ///</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeDTO>> Delete(int id)
    {
        var employeeDto = await _service.GetById(id);

        if (employeeDto is null)
            return BadRequest($"O funcionário com o ID: {id} não foi encontrado.");

        var deletedEmployeeDto = await _service.Delete(id);

        return Ok(deletedEmployeeDto);
    }
}
