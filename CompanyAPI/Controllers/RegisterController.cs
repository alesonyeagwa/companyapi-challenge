using CompanyAPI.Filters;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Schemas;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService userService;

        public RegisterController(IUserService service)
        {
            userService = service;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<User>> register([Bind("Username,Password")] RegisterRequest userReq)
        {
            
            if (!userService.UsernameExists(userReq.Username))
            {
                var user = await userService.AddUser(userReq);
                return CreatedAtAction("GetUser", new { controller = "users", id = user.Id }, user);
            }
            else
            {
                ModelState.AddModelError("Username", "A user with the username entered already exists");
                return UnprocessableEntity(ModelState);
            }
        }
    }
}
