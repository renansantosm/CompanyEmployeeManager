using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.DTOs.Models.Pagination;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Pagination;
using CompanyEmployeeManager.Repositories.Interfaces;
using CompanyEmployeeManager.Services.Interfaces;

namespace CompanyEmployeeManager.Services;

public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _repository;

    public CompanyService(IMapper mapper, ICompanyRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PagedResultListDTO<CompanyDTO>> GetAll(int pageNumber, int pageSize)
    {
        var companies = await _repository.GetAll(pageNumber, pageSize);

        var companiesDto = _mapper.Map<IEnumerable<CompanyDTO>>(companies);

        return new PagedResultListDTO<CompanyDTO>(companiesDto, new PaginationInfo(pageNumber, pageSize, await _repository.GetAllCount()));
    }

    public async Task<CompanyDTO?> GetById(int id)
    {
        var company = await _repository.GetById(id);

        return _mapper.Map<CompanyDTO>(company);
    }

    public async Task<CompanyWithAddressDTO?> GetCompanyByIdWithAddress(int id)
    {
        var company = await _repository.GetCompanyByIdWithAddress(id);

        return _mapper.Map<CompanyWithAddressDTO>(company);
    }

    public async Task<PagedResultDto<CompanyWithEmployeesPagedDTO>?> GetCompanyByIdWithEmployeesPaged(int id, int pageNumber, int pageSize)
    {
        var companyWithEmployeesPaged = await _repository.GetCompanyByIdWithEmployeesPaged(id, pageNumber, pageSize);

        if (companyWithEmployeesPaged is null)
        {
            return null;
        }

        var companyWithEmployeesPagedDto = _mapper.Map<CompanyWithEmployeesPagedDTO>(companyWithEmployeesPaged);

        return new PagedResultDto<CompanyWithEmployeesPagedDTO>(companyWithEmployeesPagedDto, new PaginationInfo(pageNumber, pageSize, 
                                                                                                                    await _repository.GetCompanyByIdWithEmployeesCount(id)));
    }

    public async Task<CompanyDTO> Create(CreateCompanyDTO companyDTO)
    {
        var company = _mapper.Map<Company>(companyDTO);
        var createdCompany = await _repository.Create(company);
        return _mapper.Map<CompanyDTO>(createdCompany);
    }

    public async Task<CompanyDTO> Update(CompanyDTO companyDTO)
    {
        var company = _mapper.Map<Company>(companyDTO);
        var updatedCompany = await _repository.Update(company);
        return _mapper.Map<CompanyDTO>(updatedCompany);
    }

    public async Task<CompanyDTO?> Delete(int id)
    {
        var deletedCompany = await _repository.Delete(id);

        return _mapper.Map<CompanyDTO>(deletedCompany);
    }
}
