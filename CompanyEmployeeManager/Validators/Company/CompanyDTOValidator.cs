using CompanyEmployeeManager.DTOs.Models.Company;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Company;

public class CompanyDTOValidator : AbstractValidator<CompanyDTO>
{
    public CompanyDTOValidator()
    {
        RuleFor(company => company.CompanyId)
            .GreaterThan(0).WithMessage("A valid ID is required");

        RuleFor(company => company.Name)
            .NotEmpty().WithMessage("Name must not be empty.")
            .Length(3, 100).WithMessage("Name must be between 2 and 100 characters long");

        RuleFor(company => company.Phone)
            .NotEmpty().WithMessage("Phone must not be empty.")
            .Length(8, 20).WithMessage("Phone must be between 8 and 20 characters long");

        RuleFor(company => company.Email)
            .NotEmpty().WithMessage("Email must not be empty.")
            .EmailAddress().WithMessage("Please provide a valid email address")
            .Length(5, 80).WithMessage("Email must be between 5 and 80 characters long.");

        RuleFor(company => company.AddressId)
            .GreaterThan(0).WithMessage("A valid AddressId is required.");
    }
}
