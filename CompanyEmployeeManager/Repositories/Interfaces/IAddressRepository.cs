using CompanyEmployeeManager.Models;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAll(int pageNumber, int pageSize);
    Task<int> GetAllCount();
    Task<Address?> GetById(int id);
    Task<Address?> GetAddressByIdWithCompaniesPaged(int id, int pageNumber, int pageSize);
    Task<int> GetAddressByIdWithCompaniesCount(int id);
    Task<Address> Create(Address address);
    Task<Address> Update(Address address);
    Task<Address?> Delete(int id);
}
