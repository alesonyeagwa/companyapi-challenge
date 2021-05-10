using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Filters;
using CompanyAPI.Models.Repositories;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IAuthenticationService authenticationRepository;

        public LoginController(IAuthenticationService _authenticationRepository)
        {
            authenticationRepository = _authenticationRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateModel]
        public ActionResult login(LoginRequest request)
        {
            LoginResponse response = authenticationRepository.Authenticsate(request);
            if(response == null)
            {
                return new JsonResult(new { message = "No account with the specified username/combination was found" }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return Ok(response);
        }
    }
}