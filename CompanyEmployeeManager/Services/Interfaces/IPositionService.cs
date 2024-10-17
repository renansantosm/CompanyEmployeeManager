using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IPositionService
{
    Task<PagedList<PositionDTO>> GetAll(int pageNumber, int pageSize);
    Task<PositionDTO> GetById(int id);
    Task<PositionWithEmployeesDTO> GetPositionWithEmployees(int id);
    Task<PositionDTO> Create(CreatePositionDTO positionDTO);
    Task<PositionDTO> Update(PositionDTO positionDTO);
    Task<PositionDTO> Delete(int id);
}
