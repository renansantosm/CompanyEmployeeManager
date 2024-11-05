using CompanyEmployeeManager.Pagination;

namespace CompanyEmployeeManager.DTOs.Models.Pagination;

public class PagedResultDto<T> where T : class
{
    public T? Data { get; set; }
    public PaginationInfo? Pagination { get; set; }

    public PagedResultDto(T? data, PaginationInfo? pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}
