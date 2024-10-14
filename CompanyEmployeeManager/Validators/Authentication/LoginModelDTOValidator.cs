using CompanyEmployeeManager.DTOs.Models.Authetication;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Authentication;

public class LoginModelDTOValidator : AbstractValidator<LoginModelDTO>
{
    public LoginModelDTOValidator()
    {
        RuleFor(login => login.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(login => login.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
