using CompanyEmployeeManager.DTOs.Models.Position;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Position;

public class CreatePositionDTOValidator : AbstractValidator<CreatePositionDTO>
{
    public CreatePositionDTOValidator()
    {
        RuleFor(position => position.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters long");

        RuleFor(position => position.Description)
            .MaximumLength(250).WithMessage("Description should not exceed 250 characters");
    }
}
