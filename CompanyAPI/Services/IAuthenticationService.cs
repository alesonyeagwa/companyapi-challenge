using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Repositories
{
    public interface IAuthenticationService
    {
        LoginResponse Authenticsate(LoginRequest request);
    }
}
