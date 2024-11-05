using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _service;
    public CompaniesController(ICompanyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retrieves a paginated list of companies.
    /// </summary>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, including <c>PageNumber</c> (the page number) and <c>PageSize</c> (the number of items per page).
    /// </param>
    /// <returns>
    /// A paginated list of companies wrapped in an <c>ActionResult</c>.
    /// If successful, returns an <c>Ok</c> (200) response with the paginated data; otherwise, returns an empty list.
    /// </returns>    
    [HttpGet]
    public async Task<ActionResult<PagedResultListDTO<CompanyDTO>>> GetAll([FromQuery] PaginationParameters paginationParams)
    {
        var companiesPagedDto = await _service.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

        return Ok(companiesPagedDto);
    }

    /// <summary>
    /// Retrieves an company by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the company to be fetched.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>CompanyDTO</c>.
    /// Returns <c>Ok</c> (200) with the address data, or <c>NotFound</c> (404) if the company is not found.
    /// </returns>
    [HttpGet("{id:int}", Name = "GetCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CompanyDTO>> Get(int id)
    {
        var companyDto = await _service.GetById(id);

        if (companyDto is null)
            return NotFound($"The company with ID: {id} was not found.");

        return Ok(companyDto);
    }

    /// <summary>
    /// Retrieves a company by its ID, including its address information.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the company to be retrieved.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing a <c>CompanyWithAddressDTO</c> object.
    /// Returns <c>Ok</c> (200) with the company and address details, or <c>NotFound</c> (404) if the company is not found.
    /// </returns>    
    [HttpGet("{id:int}/address", Name = "GetCompanyWithAddress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CompanyWithAddressDTO>> GetWithAddress(int id)
    {
        var companyWithAddressDto = await _service.GetCompanyByIdWithAddress(id);

        if (companyWithAddressDto is null)
            return NotFound($"The company with ID: {id} was not found.");

        return Ok(companyWithAddressDto);
    }

    /// <summary>
    /// Retrieves an company by its ID, including a paginated list of associated employees.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the company.
    /// </param>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, such as <c>PageNumber</c> and <c>PageSize</c>.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>CompanyWithEmployeesPagedDTO</c>.
    /// Returns <c>Ok</c> (200) with the company and employee data, or <c>NotFound</c> (404) if the company is not found.
    /// </returns>
    [HttpGet("{id:int}/employees", Name = "GetCompanyWithEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PagedResultDto<CompanyWithEmployeesPagedDTO>>> GetCompanyWithEmployees(int id, [FromQuery] PaginationParameters paginationParams)
    {
        var companyWithEmployeesPagedDto = await _service.GetCompanyByIdWithEmployeesPaged(id, paginationParams.PageNumber, paginationParams.PageSize);

        if (companyWithEmployeesPagedDto is null)
            return NotFound($"The company with ID: {id} was not found.");

        return Ok(companyWithEmployeesPagedDto);
    }

    /// <summary>
    /// Creates a new company.
    /// </summary>
    /// <param name="companyDto">
    /// The <c>CreateCompanyDTO</c> object containing the company details to be created.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the created <c>CompanyDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the input data is null, and <c>CreatedAtRoute</c> (201) if the company is successfully created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CompanyDTO>> Create(CreateCompanyDTO companyDto)
    {
        if (companyDto is null)
            return BadRequest("Company data is required and cannot be empty.");

        var createdCompanyDto = await _service.Create(companyDto);

        return new CreatedAtRouteResult("GetCompany",
            new { id = createdCompanyDto.CompanyId }, createdCompanyDto);
    }

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the company to be updated.
    /// </param>
    /// <param name="companyDto">
    /// The <c>CompanyDTO</c> object containing updated company details.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the updated <c>CompanyDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the provided ID does not match or if the input data is null, and <c>Ok</c> (200) if the update is successful.
    /// </returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CompanyDTO>> Update(int id, CompanyDTO companyDto)
    {
        if (companyDto.CompanyId != id)
            return BadRequest("The provided company ID does not match the ID in the request.");

        if(companyDto is null)
            return BadRequest("Company data cannot be empty. Please provide the necessary information for the update.");    

        var updatedCompanyDto = await _service.Update(companyDto);

        return Ok(updatedCompanyDto);
    }

    /// <summary>
    /// Deletes an company by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the company to be deleted.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the deleted <c>CompanyDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the company is not found, and <c>Ok</c> (200) if the deletion is successful.
    ///</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CompanyDTO>> Delete(int id)
    {
        var deletedCompanyDto = await _service.Delete(id);

        if (deletedCompanyDto is null)
            return BadRequest($"The company with ID: {id} was not found.");

        return Ok(deletedCompanyDto);
    }
}
