using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _service;
    public CompaniesController(ICompanyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAll(int skip = 0, int take = 10)
    {
        var companiesDto = await _service.GetAll(skip, take);

        if (companiesDto is null)
            return NotFound();

        return Ok(companiesDto);
    }

    [HttpGet("{id:int}", Name = "GetCompany")]
    public async Task<ActionResult<CompanyDTO>> Get(int id)
    {
        var companyDto = await _service.GetById(id);

        if (companyDto is null)
            return NotFound();

        return Ok(companyDto);
    }

    [HttpGet("{id:int}/address", Name = "GetCompanyWithAddress")]
    public async Task<ActionResult<CompanyWithAddressDTO>> GetWithAddress(int id)
    {
        var companyDto = await _service.GetById(id);

        if (companyDto is null)
            return NotFound();

        var companyWithAddressDto = await _service.GetCompanyAddress(id);

        return Ok(companyWithAddressDto);
    }

    [HttpGet("{id:int}/employees", Name = "GetCompanyWithEmployees")]
    public async Task<ActionResult<CompanyWithEmployeesDTO>> GetWithEmployees(int id)
    {
        var companyDto = await _service.GetById(id);

        if (companyDto is null)
            return NotFound();

        var companyWithEmployeesDetails = await _service.GetCompanyEmployees(id);

        return Ok(companyWithEmployeesDetails);
    }


    [HttpPost]
    public async Task<ActionResult<CompanyDTO>> Create(CreateCompanyDTO companyDto)
    {
        if (companyDto is null)
            return BadRequest();

        var createdCompanyDto = await _service.Create(companyDto);

        return new CreatedAtRouteResult("GetCompany",
            new { id = createdCompanyDto.CompanyId }, createdCompanyDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CompanyDTO>> Update(int id, CompanyDTO companyDto)
    {
        if (companyDto.CompanyId != id)
            return BadRequest();

        if (companyDto is null)
            return BadRequest();

        var updatedCompanyDto = await _service.Update(companyDto);

        return Ok(updatedCompanyDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CompanyDTO>> Delete(int id)
    {
        var companyDto = await _service.GetById(id);

        if (companyDto is null)
            return NotFound();

        var deletedCompanyDto = await _service.Delete(id);

        return Ok(deletedCompanyDto);
    }
}
