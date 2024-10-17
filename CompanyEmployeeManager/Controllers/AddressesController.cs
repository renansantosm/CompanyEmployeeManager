using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.Extensions;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressesController(IAddressService service)
    {
        _service = service;
    }

    //[Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery]PaginationParams paginationParams)
    {
        var addressesDto = await _service.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

        if (addressesDto is null)
            return NotFound();

        Response.AddPaginationHeader(new PaginationHeader(addressesDto.CurrentPage, addressesDto.PageSize, addressesDto.TotalCount, addressesDto.TotalPages));
        return Ok(addressesDto);
    }

    //[Authorize(Policy = "UserOnly")]
    [HttpGet("{id:int}", Name = "GetAddress")]
    public async Task<ActionResult<AddressDTO>> Get(int id)
    {
        var addressDto = await _service.GetById(id);

        if (addressDto is null)
            return NotFound();

        return Ok(addressDto);
    }

    //[Authorize]
    [HttpGet("{id:int}/companies", Name = "GetAddressWithCompanies")]
    public async Task<ActionResult<AddressWithCompaniesDTO>> GetAddressWithCompanies(int id)
    {
        var address = await _service.GetById(id);

        if (address is null)
            return NotFound();

        var addressWithCompaniesDto = await _service.GetAddressWithCompanies(id);
        return Ok(addressWithCompaniesDto);
    }


    [HttpPost]
    public async Task<ActionResult<AddressDTO>> Create(CreateAddressDTO addressDto)
    {
        if (addressDto is null)
            return BadRequest();

        var createdAddressDto = await _service.Create(addressDto);

        return new CreatedAtRouteResult("GetAddress",
            new { id = createdAddressDto.AddressId }, createdAddressDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AddressDTO>> Update(int id, AddressDTO addressDto)
    {
        if (addressDto.AddressId != id)
            return BadRequest();

        if (addressDto is null)
            return BadRequest();

        var updatedAddressDto = await _service.Update(addressDto);

        return Ok(updatedAddressDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AddressDTO>> Delete(int id)
    {
        var addressDto = await _service.GetById(id);

        if (addressDto is null)
            return NotFound();

        var deletedAddressDto = await _service.Delete(id);

        return Ok(deletedAddressDto);
    }
}
