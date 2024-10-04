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

    public async Task<IEnumerable<Address>> GetAll(int skip, int take)
    {
        return await _context.Addresses.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Address?> GetById(int id)
    {
        return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.AddressId == id);
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
            throw new ArgumentException(nameof(address));

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return address;
    }
}
