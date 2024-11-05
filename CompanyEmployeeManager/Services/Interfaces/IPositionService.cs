using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.DTOs.Models.Position;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IPositionService
{
    Task<PagedResultListDTO<PositionDTO>> GetAll(int pageNumber, int pageSize);
    Task<int> GetAllCount();
    Task<PositionDTO?> GetById(int id);
    Task<PagedResultDto<PositionWithEmployeesDTO>?> GetPositionByIdWithEmployeesPaged(int id, int pageNumber, int pageSize);
    Task<PositionDTO> Create(CreatePositionDTO positionDTO);
    Task<PositionDTO> Update(PositionDTO positionDTO);
    Task<PositionDTO?> Delete(int id);
}
