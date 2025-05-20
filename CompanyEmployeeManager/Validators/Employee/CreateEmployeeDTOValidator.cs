using CompanyEmployeeManager.DTOs.Models.Employee;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Employee;

public class CreateEmployeeDTOValidator : AbstractValidator<CreateEmployeeDTO>
{
    public CreateEmployeeDTOValidator()
    {
        RuleFor(employee => employee.Name)
            .NotEmpty().WithMessage("Name must not be empty.")
            .Length(3, 100).WithMessage("Name must be between 2 and 100 characters long");

        RuleFor(employee => employee.Age)
            .NotEmpty().WithMessage("Age must not be empty")
            .InclusiveBetween(18, 65).WithMessage("Age must be between 18 and 65 years");

        RuleFor(employee => employee.CompanyId)
            .GreaterThan(0).WithMessage("A valid CompanyId is required");

        RuleFor(employee => employee.PositionId)
            .GreaterThan(0).WithMessage("A valid PositionId is required");

    }
}
