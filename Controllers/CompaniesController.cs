using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyAPI.Data;
using CompanyAPI.Models;
using System.Net;
using System.Net.Http;
using CompanyAPI.Filters;
using CompanyAPI.Models.Schemas;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Repositories;
using CompanyAPI.Models.Responses;
using System.Threading;
using CompanyAPI.Extensions;
using CompanyAPI.Helpers;
using Microsoft.Data.SqlClient.DataClassification;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyRepository;

        public CompaniesController(ICompanyService repository)
        {
            //_context = context;
            companyRepository = repository;
        }

        // GET: api/Companies
        [HttpGet (Name = nameof(GetCompanies))]
        [ProducesResponseType(typeof(GetCompanyListResponseDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails),  400)]
        public async Task<IActionResult> GetCompanies([FromQuery] UrlQueryParameters urlQueryParameters,
            CancellationToken cancellationToken)
        {
            var companies = await companyRepository.GetByPageAsync(
                                    urlQueryParameters.Limit,
                                    urlQueryParameters.Page,
                                    cancellationToken);

            return Ok(GeneratePageLinks(urlQueryParameters, companies));
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await companyRepository.GetCompany(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // GET: api/Companies/isin/5
        [HttpGet("isin/{isin}")]
        public async Task<ActionResult<Company>> GetCompanyByISIN(string isin)
        {
            var company = await companyRepository.GetCompanyByISIN(isin);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyRequest companyReq)
        {
            Company company = await companyRepository.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }

            Company isinExists = await companyRepository.GetCompanyByISIN(company.ISIN);
            if (isinExists != null && isinExists.Id != company.Id)
            {
                ModelState.AddModelError("ISIN", "A comapny with the ISIN entered already exists");
                return BadRequest(ModelState);
            }

            //company = companyReq.UpdateCompany(company);
            await companyRepository.UpdateCompany(id, companyReq);

            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Company>> PostCompany([Bind("Name,Exchange,Ticker,ISIN")] Company company)
        {
            if (!companyRepository.CompanyISINExists(company.ISIN))
            {
                await companyRepository.AddCompany(company);
                return CreatedAtAction("GetCompany", new { id = company.Id }, company);
            }
            else
            {
                ModelState.AddModelError("ISIN", "A comapny with the ISIN entered already exists");
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            //var company = await companyRepository.GetCompany(id);
            if (!companyRepository.CompanyExists(id))
            {
                return NotFound();
            }

            await companyRepository.DeleteCompany(id);
            return NoContent();
        }

        private GetCompanyListResponseDto GeneratePageLinks(UrlQueryParameters
                     queryParameters,
                     GetCompanyListResponseDto response)
        {

            if (response.CurrentPage > 1)
            {
                var prevRoute = Url.RouteUrl(nameof(GetCompanies), new { limit = queryParameters.Limit, page = queryParameters.Page - 1 });

                response.AddResourceLink(LinkedResourceType.Prev, prevRoute);

            }

            if (response.CurrentPage < response.TotalPages)
            {
                var nextRoute = Url.RouteUrl(nameof(GetCompanies), new { limit = queryParameters.Limit, page = queryParameters.Page + 1 });

                response.AddResourceLink(LinkedResourceType.Next, nextRoute);
            }

            var linksRanges = LinksRangeMaker.Maker(response.CurrentPage, response.TotalPages);
            List<object> links = new List<object>();
            foreach (var linkNumber in linksRanges)
            {
                links.Add(new { url = linkNumber is int ? Url.RouteUrl(nameof(GetCompanies), new { limit = queryParameters.Limit, page = linkNumber }).ToLower() : null, label = linkNumber, active = linkNumber.ToString() == response.CurrentPage.ToString() ? true : false });
            }
            response.SetLinksResourceLinks(links);


            return response;
        }

    }
}
