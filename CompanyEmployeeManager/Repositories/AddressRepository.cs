using CompanyEmployeeManager.Context;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeManager.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Address>> GetAll(int pageNumber, int pageSize)
    {
        return await _context.Addresses.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await _context.Addresses.CountAsync();
    }

    public async Task<Address?> GetById(int id)
    {
        return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.AddressId == id);
    }

    public async Task<Address?> GetAddressByIdWithCompaniesPaged(int id, int pageNumber, int pageSize)
    {
        var address = await GetById(id);

        if (address is null)
        {
           return null;
        }

        address.Companies = await _context.Companies.AsNoTracking().Where(c => c.AddressId == id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return address;
    }

    public async Task<int> GetAddressByIdWithCompaniesCount(int id)
    {
        return await _context.Companies.Where(c => c.AddressId == id).CountAsync();
    }

    public async Task<Address> Create(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<Address> Update(Address address)
    {
        _context.Addresses.Update(address); 
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<Address?> Delete(int id)
    {
        var address = await _context.Addresses.FindAsync(id);

        if(address is null)
        {
            return null;
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return address;
    }
}
