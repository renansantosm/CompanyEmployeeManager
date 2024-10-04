using CompanyEmployeeManager.Models;

namespace CompanyEmployeeManager.Repositories.Interfaces;

public interface IPositionRepository
{
    Task<IEnumerable<Position>> GetAll(int skip, int take);
    Task<Position?> GetById(int id);
    Task<Position> Create(Position position);
    Task<Position> Update(Position position);
    Task<Position> Delete(int id);
}
