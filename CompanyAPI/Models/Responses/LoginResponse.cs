using CompanyAPI.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Responses
{
    public class LoginResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public LoginResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = token;
        }
    }
}
