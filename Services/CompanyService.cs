using CompanyAPI.Data;
using CompanyAPI.Extensions;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Responses;
using CompanyAPI.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Repositories
{
    public class CompanyService : ICompanyService
    {
        private readonly CompanyAPIContext _context;

        public CompanyService(CompanyAPIContext aPIContext)
        {
            this._context = aPIContext;
        }

        public async Task<Company> AddCompany(Company company)
        {
            company.CreatedAt = DateTime.Now;
            company.UpdatedAt = DateTime.Now;
            var result = await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }

        public bool CompanyISINExists(string isin)
        {
            return _context.Companies.Any(e => e.ISIN == isin);
        }

        public async Task<int> DeleteCompany(int companyId)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
            if (result != null)
            {
                _context.Companies.Remove(result);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompany(int companyId)
        {
            return await _context.Companies.FirstOrDefaultAsync(company => company.Id == companyId);
        }

        public async Task<Company> GetCompanyByISIN(string isin)
        {
            return await _context.Companies.FirstOrDefaultAsync(company => company.ISIN == isin);
        }

        public async Task<Company> UpdateCompany(int id, CompanyRequest companyReq)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                result.Name = companyReq.Name ?? result.Name;
                result.Exchange = companyReq.Exchange ?? result.Exchange;
                result.Ticker = companyReq.Ticker ?? result.Ticker;
                result.ISIN = companyReq.ISIN ?? result.ISIN;
                result.Website = companyReq.Website ?? result.Website;
                result.UpdatedAt = DateTime.Now;
                //_context.Entry(company).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public async Task<GetCompanyListResponseDto> GetByPageAsync(int limit, int page, CancellationToken cancellationToken)
        {

            var companies = await _context.Companies
                           .AsNoTracking()
                           .OrderBy(p => p.CreatedAt)
                           .PaginateAsync(page, limit, cancellationToken);

            return new GetCompanyListResponseDto
            {
                CurrentPage = companies.CurrentPage,
                TotalPages = companies.TotalPages,
                TotalItems = companies.TotalItems,
                Limit = limit,
                Items = companies.Items.Select(p => new GetCompanyResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Ticker = p.Ticker,
                    Exchange = p.Exchange,
                    ISIN = p.ISIN,
                    Website = p.Website,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                }).ToList()
            };
        }

    }
}
