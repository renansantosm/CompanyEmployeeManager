using CompanyEmployeeManager.Context;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeManager.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly AppDbContext _context;

    public PositionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Position>> GetAll(int pageNumber, int pageSize)
    {
        return await _context.Positions.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await _context.Positions.CountAsync();
    }

    public async Task<Position?> GetById(int id)
    {
        return await _context.Positions.AsNoTracking().FirstOrDefaultAsync(p => p.PositionId == id);
    }

    public async Task<Position?> GetPositionByIdWithEmployeesPaged(int id, int pageNumber, int pageSize)
    {
        var position = await GetById(id);

        if (position is null) 
        { 
            return null;    
        }

        position.Employees = await _context.Employees.AsNoTracking().Where(e => e.PositionId == id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return position;
    }

    public async Task<int> GetPositionByIdWithEmployeesCount(int id)
    {
        return await _context.Employees.Where(e => e.PositionId == id).CountAsync();
    }

    public async Task<Position> Create(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();
        return position;

    }

    public async Task<Position> Update(Position position)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync();
        return position;
    }

    public async Task<Position?> Delete(int id)
    {
        var position = await _context.Positions.FindAsync(id);

        if (position is null) 
            return null;

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return position;
    }
}
