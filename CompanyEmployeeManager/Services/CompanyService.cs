using AutoMapper;
using CompanyEmployeeManager.DTOs.Models.Company;
using CompanyEmployeeManager.Models;
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

    public async Task<IEnumerable<CompanyDTO>> GetAll(int skip, int take)
    {
        if(skip < 0) skip = 0;

        if (take <= 0) take = 10;

        var companies = await _repository.GetAll(skip, take);
        return _mapper.Map<IEnumerable<CompanyDTO>>(companies);
    }

    public async Task<CompanyDTO?> GetById(int id)
    {
        var company = await _repository.GetById(id);
        return _mapper.Map<CompanyDTO>(company);
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
