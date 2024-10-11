using CompanyEmployeeManager.Models;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAll(int skip, int take);
    Task<Address?> GetById(int id);
    Task<Address?> GetWithCompanies(int id);
    Task<Address> Create(Address address);
    Task<Address> Update(Address address);
    Task<Address?> Delete(int id);
}
