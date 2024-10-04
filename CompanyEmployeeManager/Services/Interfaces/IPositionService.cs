using CompanyEmployeeManager.DTOs.Models.Position;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IPositionService
{
    Task<IEnumerable<PositionDTO>> GetAll(int skip, int take);
    Task<PositionDTO> GetById(int id);
    Task<PositionDTO> Create(CreatePositionDTO positionDTO);
    Task<PositionDTO> Update(PositionDTO positionDTO);
    Task<PositionDTO> Delete(int id);
}
