using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IPositionRepository
{
    Task<IEnumerable<Position>> GetAll(int pageNumber, int pageSize);
    Task<int> GetAllCount();
    Task<Position?> GetById(int id);
    Task<Position?> GetPositionByIdWithEmployeesPaged(int id, int pageNumber, int pageSize);
    Task<int> GetPositionByIdWithEmployeesCount(int id);
    Task<Position> Create(Position position);
    Task<Position> Update(Position position);
    Task<Position?> Delete(int id);
}
