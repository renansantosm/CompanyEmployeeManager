using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.DTOs.Models.Pagination;

public class PagedResultListDTO<T> where T : class
{
    public IEnumerable<T> Data { get; set; }
    public PaginationInfo? Pagination { get; set; }

    public PagedResultListDTO(IEnumerable<T> data, PaginationInfo? pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}
