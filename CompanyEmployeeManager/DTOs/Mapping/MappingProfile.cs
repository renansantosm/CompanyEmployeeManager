using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Address;
using CompanyEmployeeManager.DTOs.Models.Addresses;
using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.DTOs.Models.Employee;
using CompanyEmployeeManager.DTOs.Models.Position;
using CompanyEmployeeManager.Models;

namespace CompanyEmployeeManager.DTOs.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDTO>().ReverseMap();
        CreateMap<Company, CreateCompanyDTO>().ReverseMap();

        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();

        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<Address, CreateAddressDTO>().ReverseMap();

        CreateMap<Position, PositionDTO>().ReverseMap();
        CreateMap<Position, CreatePositionDTO>().ReverseMap();
    }
}
