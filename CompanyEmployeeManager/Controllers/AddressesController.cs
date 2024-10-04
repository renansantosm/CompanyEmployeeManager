using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAll(int skip = 0, int take = 10)
    {
        var addressesDto = await _service.GetAll(skip, take);

        if (addressesDto is null)
            return NotFound();

        return Ok(addressesDto);
    }

    [HttpGet("{id:int}", Name = "GetAddress")]
    public async Task<ActionResult<AddressDTO>> Get(int id)
    {
        var addressDto = await _service.GetById(id);

        if (addressDto is null)
            return NotFound();

        return Ok(addressDto);
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
