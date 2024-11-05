using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Pagination;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IAddressService
{
    Task<PagedResultListDTO<AddressDTO>> GetAll(int pageNumber, int pageSize);
    Task<int> GetAllCount();
    Task<AddressDTO?> GetById(int id);
    Task<PagedResultDto<AddressWithCompaniesDTO>?> GetAddressByIdWithCompaniesPaged(int id, int pageNumber, int pageSize);
    Task<AddressDTO> Create(CreateAddressDTO addressDTO);
    Task<AddressDTO> Update(AddressDTO addressDTO);
    Task<AddressDTO?> Delete(int id);
}
