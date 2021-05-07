using CompanyAPI.Models.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public class CompanyRequest
    {
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string Exchange { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Ticker { get; set; }
        [RegularExpression(@"^[a-zA-Z]{2}\d+$")]
        public string ISIN { get; set; }
        [RegularExpression(@"^(https?:\/\/)?([\w\d-_]+)\.([\w\d-_\.]+)\/?\??([^#\n\r]*)?#?([^\n\r]*)$")]
        public string Website { get; set; }

        public Company GetCompany()
        {
            return new Company() { Name = this.Name, Exchange = this.Exchange, Ticker = this.Ticker, ISIN = this.ISIN, Website = this.Website, CreatedAt = DateTime.Now };
        }

        public Company UpdateCompany(Company company)
        {
            company.Name = Name ?? company.Name;
            company.Exchange = Exchange ?? company.Exchange;
            company.Ticker = Ticker ?? company.Ticker;
            company.ISIN = ISIN ?? company.ISIN;
            company.Website = Website ?? company.Website;
            return company;
        }
    }
}
