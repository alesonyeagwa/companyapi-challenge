using CompanyAPI.Extensions;
using CompanyAPI.Interfaces;
using CompanyAPI.Models.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Responses
{
    public record GetCompanyResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        public string ISIN { get; set; }
        public string Website { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public record GetCompanyListResponseDto : ILinkedResource
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public int Limit { get; init; }
        public List<GetCompanyResponseDto> Items { get; init; }
        public IDictionary<LinkedResourceType, object> Links { get ; set; }
    }
}
