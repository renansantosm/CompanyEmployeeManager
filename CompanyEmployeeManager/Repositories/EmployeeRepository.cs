using CompanyEmployeeManager.Context;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeManager.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAll(int skip, int take)
    {
        return await _context.Employees.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Employee?> GetById(int id)
    {
        return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(p => p.EmployeeId == id);
    }
    public async Task<Employee> Create(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;

    }

    public async Task<Employee> Update(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if(employee is null) 
            throw new ArgumentException(nameof(employee));

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
}
