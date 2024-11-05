using CompanyEmployeeManager.Pagination;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Pagination;

public class PaginationParametersValidator : AbstractValidator<PaginationParameters>
{
    public PaginationParametersValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be greater than or equal to 1.");

        RuleFor(p => p.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("PageSize cannot be greater than 20.");
    }
}
