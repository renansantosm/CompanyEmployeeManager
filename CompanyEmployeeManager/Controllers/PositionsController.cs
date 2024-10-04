using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionsController : ControllerBase
{
    private readonly IPositionService _service;

    public PositionsController(IPositionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionDTO>>> GetAll(int skip = 0, int take = 10)
    {
        var positionsDto = await _service.GetAll(skip, take);

        if (positionsDto is null)
            return NotFound();

        return Ok(positionsDto);
    }

    [HttpGet("{id:int}", Name = "GetPosition")]
    public async Task<ActionResult<PositionDTO>> Get(int id)
    {
        var positionDto = await _service.GetById(id);

        if (positionDto is null)
            return NotFound();

        return Ok(positionDto);
    }

    [HttpPost]
    public async Task<ActionResult<PositionDTO>> Create(CreatePositionDTO positionDTO)
    {
        if (positionDTO is null)
            return BadRequest();

        var createdPositionDto = await _service.Create(positionDTO);

        return new CreatedAtRouteResult("GetPosition",
            new { id = createdPositionDto.PositionId }, createdPositionDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PositionDTO>> Update(int id, PositionDTO positionDto)
    {
        if (positionDto.PositionId != id)
            return BadRequest();

        if (positionDto is null)
            return BadRequest();

        var updatedPositionDto = await _service.Update(positionDto);

        return Ok(updatedPositionDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PositionDTO>> Delete(int id)
    {
        var positionDto = await _service.GetById(id);

        if (positionDto is null)
            return NotFound();

        var deletedPositionDto = await _service.Delete(id);

        return Ok(deletedPositionDto);
    }
}
