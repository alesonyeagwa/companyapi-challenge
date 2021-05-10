using CompanyAPI.Models.Schemas;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        public string ISIN { get; set; }
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

    public class CreateCompanyRequest : CompanyRequest
    {
        public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
        {
            public CreateCompanyRequestValidator()
            {
                RuleFor(o => o.Name).NotEmpty().MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.Exchange).NotEmpty().MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.Ticker).NotEmpty().MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.ISIN).NotEmpty().Matches(@"^[a-zA-Z]{2}\d+$")
                                    .WithMessage("The first two characters of an ISIN must be letters / non numeric");
                RuleFor(o => o.Website).Matches(@"^(https?:\/\/)?([\w\d-_]+)\.([\w\d-_\.]+)\/?\??([^#\n\r]*)?#?([^\n\r]*)$")
                                        .WithMessage("The website must be a valid URL");
            }
        }
    }

    public class UpdateCompanyRequest : CompanyRequest
    {
        public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
        {
            public UpdateCompanyRequestValidator()
            {
                RuleFor(o => o.Name).MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.Exchange).MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.Ticker).MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.ISIN).Matches(@"^[a-zA-Z]{2}\d+$")
                                    .WithMessage("The first two characters of an ISIN must be letters / non numeric");
                RuleFor(o => o.Website).Matches(@"^(https?:\/\/)?([\w\d-_]+)\.([\w\d-_\.]+)\/?\??([^#\n\r]*)?#?([^\n\r]*)$")
                                        .WithMessage("The website must be a valid URL");
            }
        }
    }
}
