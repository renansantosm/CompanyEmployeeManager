using CompanyEmployeeManager.Models;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Pagination;

public class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(p => p.PageSize)
            .InclusiveBetween(1, 25).WithMessage("Page size must be between 1 and 25.");
    }
}
