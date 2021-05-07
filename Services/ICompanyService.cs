using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Responses;
using CompanyAPI.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Repositories
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int companyId);
        Task<Company> GetCompanyByISIN(string isin);
        Task<Company> AddCompany(Company company);
        Task<Company> UpdateCompany(int id, CompanyRequest companyReq);
        Task<GetCompanyListResponseDto> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
        Task<int> DeleteCompany(int companyId);
        bool CompanyExists(int id);
        bool CompanyISINExists(string id);
    }
}
