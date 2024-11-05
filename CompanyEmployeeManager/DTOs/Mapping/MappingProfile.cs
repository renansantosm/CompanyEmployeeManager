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
        // Company
        CreateMap<Company, CompanyDTO>().ReverseMap();
        CreateMap<Company, CreateCompanyDTO>().ReverseMap();
        CreateMap<Company, CompanyWithoutAddressDTO>().ReverseMap();

        CreateMap<Company, CompanyWithAddressDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

        CreateMap<Company, CompanyWithEmployeesPagedDTO>()
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees))
                .ReverseMap();

        // Employee
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
        CreateMap<Employee, EmployeeWithoutCompanyDTO>().ReverseMap();
        CreateMap<Employee, EmployeeWithoutPositionDTO>().ReverseMap();

        CreateMap<Employee, EmployeeWithPositionDTO>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ReverseMap();

        // Address
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<Address, CreateAddressDTO>().ReverseMap();

        CreateMap<Address, AddressWithCompaniesDTO>()
            .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Companies))
                .ReverseMap();

        // Position
        CreateMap<Position, PositionDTO>().ReverseMap();
        CreateMap<Position, CreatePositionDTO>().ReverseMap();

        CreateMap<Position, PositionWithEmployeesDTO>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees))
                    .ReverseMap();
    }
}
