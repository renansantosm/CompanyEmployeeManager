using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Authetication;
using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Validators.Address;
using CompanyEmployeeManager.Validators.Authentication;
using CompanyEmployeeManager.Validators.Company;
using CompanyEmployeeManager.Validators.Employee;
using CompanyEmployeeManager.Validators.Pagination;
using CompanyEmployeeManager.Validators.Position;
using FluentValidation;

namespace CompanyEmployeeManager.Extensions;

public static class ValidationDependencyInjectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        // Company
        services.AddScoped<IValidator<CompanyDTO>, CompanyDTOValidator>();
        services.AddScoped<IValidator<CreateCompanyDTO>, CreateCompanyDTOValidator>();

        // Address
        services.AddScoped<IValidator<AddressDTO>, AddressDTOValidator>();
        services.AddScoped<IValidator<CreateAddressDTO>, CreateAddressDTOValidator>();

        // Employee
        services.AddScoped<IValidator<CreateEmployeeDTO>, CreateEmployeeDTOValidator>();
        services.AddScoped<IValidator<EmployeeDTO>, EmployeeDTOValidator>();

        // Position
        services.AddScoped<IValidator<PositionDTO>, PositionDTOValidator>();
        services.AddScoped<IValidator<CreatePositionDTO>, CreatePositionDTOValidator>();

        // Authentication
        services.AddScoped<IValidator<RegisterModelDTO>, RegisterModelDTOValidator>();
        services.AddScoped<IValidator<LoginModelDTO>, LoginModelDTOValidator>();

        // Pagination
        services.AddScoped<IValidator<PaginationParameters>, PaginationParametersValidator>();

        return services;
    }
}
