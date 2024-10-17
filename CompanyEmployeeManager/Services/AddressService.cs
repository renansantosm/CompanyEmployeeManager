using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Repositories.Interfaces;
using CompanyEmployeeManager.Services.Interfaces;

namespace CompanyEmployeeManager.Services;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _repository;

    public AddressService(IMapper mapper, IAddressRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PagedList<AddressDTO>> GetAll(int pageNumber, int pageSize)
    {
        var addresses = await _repository.GetAll(pageNumber, pageSize);
        var addressesDto = _mapper.Map<IEnumerable<AddressDTO>>(addresses);

        return new PagedList<AddressDTO>(addressesDto, pageNumber, pageSize, addresses.TotalCount);
    }

    public async Task<AddressDTO> GetById(int id)
    {
        var address = await _repository.GetById(id);
        return _mapper.Map<AddressDTO>(address);
    }

    public async Task<AddressWithCompaniesDTO> GetAddressWithCompanies(int id)
    {
        var address = await _repository.GetWithCompanies(id);
        return _mapper.Map<AddressWithCompaniesDTO>(address);
    }

    public async Task<AddressDTO> Create(CreateAddressDTO addressDTO)
    {
        var address = _mapper.Map<Address>(addressDTO);
        var createdAddress = await _repository.Create(address);
        return _mapper.Map<AddressDTO>(createdAddress);
    }
    public async Task<AddressDTO> Update(AddressDTO addressDTO)
    {
        var address = _mapper.Map<Address>(addressDTO);
        var updatedAddress = await _repository.Update(address);
        return _mapper.Map<AddressDTO>(updatedAddress);
    }

    public async Task<AddressDTO> Delete(int id)
    {
        var deletedAddress = await _repository.Delete(id);
        return _mapper.Map<AddressDTO>(deletedAddress);
    }
}
