using CompanyEmployeeManager.Services.Interfaces;
using CompanyEmployeeManager.Services;

namespace CompanyEmployeeManager.Extensions;

public static class ServiceDependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
