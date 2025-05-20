using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressesController(IAddressService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retrieves a paginated list of addresses.
    /// </summary>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, including <c>PageNumber</c> (the page number) and <c>PageSize</c> (the number of items per page).
    /// </param>
    /// <returns>
    /// A paginated list of address wrapped in an <c>ActionResult</c>.
    /// If successful, returns an <c>Ok</c> (200) response with the paginated data; otherwise, returns an empty list.
    /// </returns>    
    /// //[Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<PagedResultListDTO<AddressDTO>>> GetAll([FromQuery] PaginationParameters paginationParams)
    {
        var addressesPagedDto = await _service.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

        return Ok(addressesPagedDto);
    }

    /// <summary>
    /// Retrieves an address by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the address to be fetched.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>AddressDTO</c>.
    /// Returns <c>Ok</c> (200) with the address data, or <c>NotFound</c> (404) if the address is not found.
    /// </returns>
    //[Authorize(Policy = "UserOnly")]
    [HttpGet("{id:int}", Name = "GetAddress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AddressDTO>> Get(int id)
    {
        var addressDto = await _service.GetById(id);

        if (addressDto is null)
            return NotFound($"The address with ID: {id} was not found.");

        return Ok(addressDto);
    }

    /// <summary>
    /// Retrieves an address by its ID, including a paginated list of associated companies.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the address.
    /// </param>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, such as <c>PageNumber</c> and <c>PageSize</c>.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>AddressWithCompaniesDTO</c>.
    /// Returns <c>Ok</c> (200) with the address and company data, or <c>NotFound</c> (404) if the address is not found.
    /// </returns>
    //[Authorize]
    [HttpGet("{id:int}/companies", Name = "GetAddressWithCompanies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PagedResultDto<AddressWithCompaniesDTO>>> GetAddressWithCompanies(int id, [FromQuery] PaginationParameters paginationParams)
    {
        var addressWithCompaniesPagedDto = await _service.GetAddressByIdWithCompaniesPaged(id, paginationParams.PageNumber, paginationParams.PageSize);

        if (addressWithCompaniesPagedDto is null)
            return NotFound($"The address with ID: {id} was not found.");

        return Ok(addressWithCompaniesPagedDto);
    }

    /// <summary>
    /// Creates a new address.
    /// </summary>
    /// <param name="addressDto">
    /// The <c>CreateAddressDTO</c> object containing the address details to be created.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the created <c>AddressDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the input data is null, and <c>CreatedAtRoute</c> (201) if the address is successfully created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AddressDTO>> Create(CreateAddressDTO addressDto)
    {
        if (addressDto is null)
            return BadRequest("Address data is required and cannot be empty.");

        var createdAddressDto = await _service.Create(addressDto);

        return new CreatedAtRouteResult("GetAddress",
            new { id = createdAddressDto.AddressId }, createdAddressDto);
    }

    /// <summary>
    /// Updates an existing address.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the address to be updated.
    /// </param>
    /// <param name="addressDto">
    /// The <c>AddressDTO</c> object containing updated address details.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the updated <c>AddressDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the provided ID does not match or if the input data is null, and <c>Ok</c> (200) if the update is successful.
    /// </returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AddressDTO>> Update(int id, AddressDTO addressDto)
    {
        if (addressDto.AddressId != id)
            return BadRequest("The provided address ID does not match the ID in the request.");

        if (addressDto is null)
            return BadRequest("Address data cannot be empty. Please provide the necessary information for the update.");

        var updatedAddressDto = await _service.Update(addressDto);

        return Ok(updatedAddressDto);
    }

    /// <summary>
    /// Deletes an address by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the address to be deleted.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the deleted <c>AddressDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the address is not found, and <c>Ok</c> (200) if the deletion is successful.
    ///</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AddressDTO>> Delete(int id)
    {
        var addressDto = await _service.GetById(id);

        if (addressDto is null)
            return BadRequest($"The address with ID: {id} was not found.");

        var deletedAddressDto = await _service.Delete(id);

        return Ok(deletedAddressDto);
    }
}
