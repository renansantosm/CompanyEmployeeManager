using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Addresses;
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

    public async Task<PagedList<PositionDTO>> GetAll(int pageNumber, int pageSize)
    {
        var positions = await _repository.GetAll(pageNumber, pageSize);
        var positionDto = _mapper.Map<IEnumerable<PositionDTO>>(positions);

        return new PagedList<PositionDTO>(positionDto, pageNumber, pageSize, positions.TotalCount);
    }

    public async Task<PositionDTO> GetById(int id)
    {
        var position = await _repository.GetById(id);
        return _mapper.Map<PositionDTO>(position);

    }
    public async Task<PositionWithEmployeesDTO> GetPositionWithEmployees(int id)
    {
        var position = await _repository.GetWithEmployees(id);
        return _mapper.Map<PositionWithEmployeesDTO>(position);
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

    public async Task<PositionDTO> Delete(int id)
    {
        var deletedPosition = await _repository.Delete(id);
        return _mapper.Map<PositionDTO>(deletedPosition);
    }
}
