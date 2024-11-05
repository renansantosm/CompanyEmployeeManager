using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Repositories.Interfaces;
using CompanyEmployeeManager.Services.Interfaces;

namespace CompanyEmployeeManager.Services;

public class PositionService : IPositionService
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _repository;

    public PositionService(IMapper mapper, IPositionRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PagedResultListDTO<PositionDTO>> GetAll(int pageNumber, int pageSize)
    {
        var positions = await _repository.GetAll(pageNumber, pageSize);

        var positionsDto = _mapper.Map<IEnumerable<PositionDTO>>(positions);

        return new PagedResultListDTO<PositionDTO>(positionsDto, new PaginationInfo(pageNumber, pageSize, await _repository.GetAllCount()));
    }

    public async Task<int> GetAllCount()
    {
        return await _repository.GetAllCount();
    }

    public async Task<PositionDTO?> GetById(int id)
    {
        var position = await _repository.GetById(id);
        return _mapper.Map<PositionDTO>(position);

    }
    public async Task<PagedResultDto<PositionWithEmployeesDTO>?> GetPositionByIdWithEmployeesPaged(int id, int pageNumber, int pageSize)
    {
        var positionWithEmployeesPaged = await _repository.GetPositionByIdWithEmployeesPaged(id, pageNumber, pageSize);

        if(positionWithEmployeesPaged is null)
        {
            return null;
        }

        var positionWithEmployeesPagedDto = _mapper.Map<PositionWithEmployeesDTO>(positionWithEmployeesPaged);

        return new PagedResultDto<PositionWithEmployeesDTO>(positionWithEmployeesPagedDto, new PaginationInfo(pageNumber, pageSize, 
                                                                                                                    await _repository.GetPositionByIdWithEmployeesCount(id)));
    }

    public async Task<PositionDTO> Create(CreatePositionDTO positionDTO)
    {
        var position = _mapper.Map<Position>(positionDTO);
        var createdPosition = await _repository.Create(position);
        return _mapper.Map<PositionDTO>(createdPosition);
    }

    public async Task<PositionDTO> Update(PositionDTO positionDTO)
    {
        var position = _mapper.Map<Position>(positionDTO);
        var updatedPosition = await _repository.Update(position);   
        return _mapper.Map<PositionDTO>(updatedPosition);
    }

    public async Task<PositionDTO?> Delete(int id)
    {
        var deletedPosition = await _repository.Delete(id);

        return _mapper.Map<PositionDTO>(deletedPosition);
    }
}
