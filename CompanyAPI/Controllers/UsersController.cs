using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyAPI.Data;
using CompanyAPI.Models.Schemas;
using CompanyAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using CompanyAPI.Services;
using CompanyAPI.Models.Requests;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService service)
        {
            userService = service;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = nameof(GetUser))]
        [Filters.Authorize]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var _user = (User)Request.HttpContext.Items["User"];

            if (_user.Id != id)
            {
                return Unauthorized();
            }

            var user = userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
