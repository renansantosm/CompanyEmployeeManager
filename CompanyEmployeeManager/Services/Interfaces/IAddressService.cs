using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<AddressDTO>> GetAll(int skip, int take);
    Task<AddressDTO> GetById(int id);
    Task<AddressWithCompaniesDTO> GetAddressWithCompanies(int id);
    Task<AddressDTO> Create(CreateAddressDTO addressDTO);
    Task<AddressDTO> Update(AddressDTO addressDTO);
    Task<AddressDTO> Delete(int id);
}
