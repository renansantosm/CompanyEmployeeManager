namespace CompanyEmployeeManager.DTOs.Models.Position;

public class PositionDTO
{
    public int PositionId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
