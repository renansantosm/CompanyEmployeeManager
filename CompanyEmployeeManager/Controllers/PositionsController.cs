using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class PositionsController : ControllerBase
{
    private readonly IPositionService _service;

    public PositionsController(IPositionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retrieves a paginated list of positions.
    /// </summary>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, including <c>PageNumber</c> (the page number) and <c>PageSize</c> (the number of items per page).
    /// </param>
    /// <returns>
    /// A paginated list of positions wrapped in an <c>ActionResult</c>.
    /// If successful, returns an <c>Ok</c> (200) response with the paginated data; otherwise, returns an empty list.
    /// </returns>    
    [HttpGet]
    public async Task<ActionResult<PagedResultListDTO<PositionDTO>>> GetAll([FromQuery] PaginationParameters paginationParams)
    {
        var positionsPagedDto = await _service.GetAll(paginationParams.PageNumber, paginationParams.PageSize);

        return Ok(positionsPagedDto);
    }

    /// <summary>
    /// Retrieves an position by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the position to be fetched.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>PositionDTO</c>.
    /// Returns <c>Ok</c> (200) with the position data, or <c>NotFound</c> (404) if the position is not found.
    /// </returns>
    [HttpGet("{id:int}", Name = "GetPosition")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PositionDTO>> Get(int id)
    {
        var positionDto = await _service.GetById(id);

        if (positionDto is null)
            return NotFound($"The position with ID: {id} was not found.");

        return Ok(positionDto);
    }

    /// <summary>
    /// Retrieves an position by its ID, including a paginated list of associated employees.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the position.
    /// </param>
    /// <param name="paginationParams">
    /// An object containing pagination parameters, such as <c>PageNumber</c> and <c>PageSize</c>.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing an <c>PositionWithEmployeesDTO</c>.
    /// Returns <c>Ok</c> (200) with the position and employee data, or <c>NotFound</c> (404) if the position is not found.
    /// </returns>
    [HttpGet("{id:int}/employees", Name = "GetPositionWithEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PagedResultDto<PositionWithEmployeesDTO>>> GetPositionWithEmployees(int id, [FromQuery] PaginationParameters paginationParams)
    {
        var positionWithEmployeesPagedDto = await _service.GetPositionByIdWithEmployeesPaged(id, paginationParams.PageNumber, paginationParams.PageSize);

        if (positionWithEmployeesPagedDto is null)
            return NotFound($"The position with ID: {id} was not found.");

        return Ok(positionWithEmployeesPagedDto);
    }

    /// <summary>
    /// Creates a new position.
    /// </summary>
    /// <param name="positionDTO">
    /// The <c>CreatePositionDTO</c> object containing the position details to be created.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the created <c>PositionDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the input data is null, and <c>CreatedAtRoute</c> (201) if the position is successfully created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PositionDTO>> Create(CreatePositionDTO positionDTO)
    {
        if (positionDTO is null)
            return BadRequest("Position data is required and cannot be empty.");

        var createdPositionDto = await _service.Create(positionDTO);

        return new CreatedAtRouteResult("GetPosition",
            new { id = createdPositionDto.PositionId }, createdPositionDto);
    }

    /// <summary>
    /// Updates an existing position.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the position to be updated.
    /// </param>
    /// <param name="positionDto">
    /// The <c>PositionDTO</c> object containing updated position details.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the updated <c>PositionDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the provided ID does not match or if the input data is null, and <c>Ok</c> (200) if the update is successful.
    /// </returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PositionDTO>> Update(int id, PositionDTO positionDto)
    {
        if (positionDto.PositionId != id)
            return BadRequest("The provided position ID does not match the ID in the request");

        if (positionDto is null)
            return BadRequest("Position data cannot be empty. Please provide the necessary information for the update.");

        var updatedPositionDto = await _service.Update(positionDto);

        return Ok(updatedPositionDto);
    }

    /// <summary>
    /// Deletes an position by its ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the position to be deleted.
    /// </param>
    /// <returns>
    /// An <c>ActionResult</c> containing the deleted <c>PositionDTO</c>.
    /// Returns <c>BadRequest</c> (400) if the position is not found, and <c>Ok</c> (200) if the deletion is successful.
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PositionDTO>> Delete(int id)
    {
        var deletedPositionDto = await _service.Delete(id);

        if (deletedPositionDto is null)
            return BadRequest($"The position with ID: {id} was not found..");

        return Ok(deletedPositionDto);
    }
}
