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

    public async Task<IEnumerable<Company>> GetAll(int pageNumber, int pageSize)
    {
        return await _context.Companies.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await _context.Companies.CountAsync();
    }

    public async Task<Company?> GetById(int id)
    {
        return await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.CompanyId == id);
    }

    public async Task<Company?> GetCompanyByIdWithAddress(int id)
    {
        var company = await GetById(id);

        if (company is null)
        {
            return null;
        }

        return await _context.Companies.AsNoTracking().Include(c => c.Address).Where(c => c.CompanyId == id).FirstOrDefaultAsync();
    }

    public async Task<Company?> GetCompanyByIdWithEmployeesPaged(int id, int pageNumber, int pageSize)
    {
        var company = await GetById(id);

        if (company is null)
        {
            return null;
        }

        company.Employees = await _context.Employees.AsNoTracking().Where(c => c.CompanyId == id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return company;
    }

    public async Task<int> GetCompanyByIdWithEmployeesCount(int id)
    {
        return await _context.Employees.Where(e => e.CompanyId == id).CountAsync();
    }

    public async Task<Company> Create(Company company)
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

    public async Task<Company?> Delete(int id)
    {
        var company = await _context.Companies.FindAsync(id);

        if(company is null) 
            return null;

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return company;
    }
}
