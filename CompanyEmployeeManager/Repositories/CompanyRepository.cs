using CompanyEmployeeManager.Context;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeManager.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAll(int skip, int take)
    {
        return await _context.Companies.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Company?> GetById(int id)
    {
        return await _context.Companies.AsNoTracking().FirstOrDefaultAsync(p => p.CompanyId == id);
    }

    public async Task<Company?> GetWithAddress(int id)
    {
        return await _context.Companies.AsNoTracking().Include(c => c.Address).Where(c => c.CompanyId == id).FirstOrDefaultAsync();
    }

    public async Task<Company?> GetWithEmployees(int id)
    {
        return await _context.Companies.AsNoTracking().Include(c => c.Employees).Where(c => c.CompanyId == id).FirstOrDefaultAsync();
    }

    public async Task<Company?> Create(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> Update(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> Delete(int id)
    {
        var company = await _context.Companies.FindAsync(id);

        if(company is null) 
            throw new ArgumentException(nameof(company));

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return company;
    }
}
