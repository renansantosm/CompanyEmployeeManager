using CompanyEmployeeManager.DTOs.Models.Address;
using FluentValidation;

namespace CompanyEmployeeManager.Validators.Address;

public class CreateAddressDTOValidator : AbstractValidator<CreateAddressDTO>
{
    public CreateAddressDTOValidator()
    {
        RuleFor(address => address.Street)
            .NotEmpty().WithMessage("Street name is required ")
            .Length(5, 120).WithMessage("Street name must be between 5 and 120 characters long");

        RuleFor(address => address.Number)
            .NotEmpty().WithMessage("Address number is required")
            .Length(1, 10).WithMessage("Address number must be between 1 and 10 characters long");

        RuleFor(address => address.City)
            .NotEmpty().WithMessage("Address city is required")
            .Length(3, 50).WithMessage("Address city must be between 3 and 50 characters long");

        RuleFor(address => address.State)
            .NotEmpty().WithMessage("Address state is required")
            .Length(3, 50).WithMessage("Address state must be between 3 and 50 characters long");


        RuleFor(address => address.Country)
            .NotEmpty().WithMessage("Address country is required")
            .Length(3, 50).WithMessage("Address country must be between 3 and 50 characters long");

        RuleFor(address => address.PostalCode)
            .NotEmpty().WithMessage("Address Postal Code is required")
            .Length(5, 12).WithMessage("Address Postal Code must be between 5 and 12 characters long");
    }
}
