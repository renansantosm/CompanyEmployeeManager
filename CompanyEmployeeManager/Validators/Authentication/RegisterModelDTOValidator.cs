using CompanyEmployeeManager.DTOs.Models.Authetication;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Authentication;

public class RegisterModelDTOValidator : AbstractValidator<RegisterModelDTO>
{
    public RegisterModelDTOValidator()
    {
        RuleFor(register => register.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(register => register.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please provide a valid email address");

        RuleFor(register => register.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Your password length must be at least 5.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.\#]+").WithMessage("Your password must contain at least one of the following: ! ? * . #");
    }
}
