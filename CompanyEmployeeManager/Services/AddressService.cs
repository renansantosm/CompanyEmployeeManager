using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Pagination;
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

    public async Task<PagedResultListDTO<AddressDTO>> GetAll(int pageNumber, int pageSize)
    {
        var addresses = await _repository.GetAll(pageNumber, pageSize);

        var addressesPagedDto = _mapper.Map<IEnumerable<AddressDTO>>(addresses);

        return new PagedResultListDTO<AddressDTO>(addressesPagedDto, new PaginationInfo(pageNumber, pageSize, await _repository.GetAllCount()));
    }

    public async Task<int> GetAllCount()
    {
        return await _repository.GetAllCount();
    }

    public async Task<AddressDTO?> GetById(int id)
    {
        var address = await _repository.GetById(id);
        return _mapper.Map<AddressDTO>(address);
    }

    public async Task<PagedResultDto<AddressWithCompaniesDTO>?> GetAddressByIdWithCompaniesPaged(int id, int pageNumber, int pageSize)
    {
        var addressWithCompaniesPaged = await _repository.GetAddressByIdWithCompaniesPaged(id, pageNumber, pageSize);

        if(addressWithCompaniesPaged is null)
        {
            return null;
        }

        var addressWithCompaniesPAgedDto = _mapper.Map<AddressWithCompaniesDTO>(addressWithCompaniesPaged);

        return new PagedResultDto<AddressWithCompaniesDTO>(addressWithCompaniesPAgedDto, new PaginationInfo(pageNumber, pageSize, 
                                                                                                await _repository.GetAddressByIdWithCompaniesCount(id)));
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

    public async Task<AddressDTO?> Delete(int id)
    {
        var deletedAddress = await _repository.Delete(id);
        return _mapper.Map<AddressDTO>(deletedAddress);
    }
}
