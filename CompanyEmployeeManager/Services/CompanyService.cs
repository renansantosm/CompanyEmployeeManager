using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Company;
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

    public async Task<PagedList<CompanyDTO>> GetAll(int pageNumber, int pageSize)
    {
        var companies = await _repository.GetAll(pageNumber, pageSize);
        var companiesDto =  _mapper.Map<IEnumerable<CompanyDTO>>(companies);

        return new PagedList<CompanyDTO>(companiesDto, pageNumber, pageSize, companies.TotalCount);
    }

    public async Task<CompanyDTO?> GetById(int id)
    {
        var company = await _repository.GetById(id);
        return _mapper.Map<CompanyDTO>(company);
    }

    public async Task<CompanyWithAddressDTO?> GetCompanyAddress(int id)
    {
        var company = await _repository.GetWithAddress(id);
        return _mapper.Map<CompanyWithAddressDTO>(company);

    }

    public async Task<CompanyWithEmployeesDTO?> GetCompanyEmployees(int id)
    {
        var company = await _repository.GetWithEmployees(id);
        return _mapper.Map<CompanyWithEmployeesDTO>(company);
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

    public async Task<CompanyDTO> Delete(int id)
    {
        var deletedCompany = await _repository.Delete(id);
        return _mapper.Map<CompanyDTO>(deletedCompany);
    }
}
