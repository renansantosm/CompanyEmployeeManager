using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IPositionRepository
{
    Task<PagedList<Position>> GetAll(int pageNumber, int pageSize);
    Task<Position?> GetById(int id);
    Task<Position?> GetWithEmployees(int id);
    Task<Position> Create(Position position);
    Task<Position> Update(Position position);
    Task<Position> Delete(int id);
}
