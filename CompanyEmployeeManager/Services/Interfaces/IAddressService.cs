using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IAddressService
{
    Task<PagedList<AddressDTO>> GetAll(int pageNumber, int pageSize);
    Task<AddressDTO> GetById(int id);
    Task<AddressWithCompaniesDTO> GetAddressWithCompanies(int id);
    Task<AddressDTO> Create(CreateAddressDTO addressDTO);
    Task<AddressDTO> Update(AddressDTO addressDTO);
    Task<AddressDTO> Delete(int id);
}
