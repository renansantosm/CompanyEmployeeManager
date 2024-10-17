using CompanyEmployeeManager.Context;
using CompanyEmployeeManager.Helpers;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CompanyEmployeeManager.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly AppDbContext _context;

    public PositionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Position>> GetAll(int pageNumber, int pageSize)
    {
        var query = _context.Positions.AsQueryable();

        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
    }

    public async Task<Position?> GetById(int id)
    {
        return await _context.Positions.AsNoTracking().FirstOrDefaultAsync(p => p.PositionId == id);
    }

    public async Task<Position?> GetWithEmployees(int id)
    {
        return await _context.Positions.AsNoTracking().Include(p => p.Employees).Where(p => p.PositionId == id).FirstOrDefaultAsync();
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

    public async Task<Position> Delete(int id)
    {
        var position = await _context.Positions.FindAsync(id);

        if (position is null) 
            throw new ArgumentException(nameof(position));

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return position;
    }
}
