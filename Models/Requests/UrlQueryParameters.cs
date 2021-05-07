using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public record UrlQueryParameters(int Limit = 50, int Page = 1);
}
