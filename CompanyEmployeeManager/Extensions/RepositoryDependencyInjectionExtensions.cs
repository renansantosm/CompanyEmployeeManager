using CompanyEmployeeManager.Repositories.Interfaces;
using CompanyEmployeeManager.Repositories;

namespace CompanyEmployeeManager.Extensions;

public static class RepositoryDependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        
        return services;
    }
}
