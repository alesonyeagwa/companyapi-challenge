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
        Task<Company> GetCompany(long companyId);
        Task<Company> GetCompanyByISIN(string isin);
        Task<Company> AddCompany(CreateCompanyRequest company);
        Task<Company> UpdateCompany(long id, CompanyRequest companyReq);
        Task<GetCompanyListResponseDto> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
        Task<bool> DeleteCompany(long companyId);
        bool CompanyExists(long id);
        bool CompanyISINExists(string isin);
    }
}
