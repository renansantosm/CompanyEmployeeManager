using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<PagedList<Address>> GetAll(int pageNumber, int pageSize);
    Task<Address?> GetById(int id);
    Task<Address?> GetWithCompanies(int id);
    Task<Address> Create(Address address);
    Task<Address> Update(Address address);
    Task<Address?> Delete(int id);
}
