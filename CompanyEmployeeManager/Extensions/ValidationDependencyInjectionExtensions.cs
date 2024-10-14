using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Authetication;
using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Validators.Address;
using CompanyEmployeeManager.Validators.Authentication;
using CompanyEmployeeManager.Validators.Company;
using CompanyEmployeeManager.Validators.Employee;
using CompanyEmployeeManager.Validators.Position;
using FluentValidation;

namespace CompanyEmployeeManager.Extensions;

public static class ValidationDependencyInjectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CompanyDTO>, CompanyDTOValidator>();
        services.AddScoped<IValidator<CreateCompanyDTO>, CreateCompanyDTOValidator>();

        services.AddScoped<IValidator<AddressDTO>, AddressDTOValidator>();
        services.AddScoped<IValidator<CreateAddressDTO>, CreateAddressDTOValidator>();

        services.AddScoped<IValidator<CreateEmployeeDTO>, CreateEmployeeDTOValidator>();
        services.AddScoped<IValidator<EmployeeDTO>, EmployeeDTOValidator>();

        services.AddScoped<IValidator<PositionDTO>, PositionDTOValidator>();
        services.AddScoped<IValidator<CreatePositionDTO>, CreatePositionDTOValidator>();

        services.AddScoped<IValidator<RegisterModelDTO>, RegisterModelDTOValidator>();
        services.AddScoped<IValidator<LoginModelDTO>, LoginModelDTOValidator>();

        return services;
    }
}
