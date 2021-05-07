using CompanyAPI.Models.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public class UserRequest
    {
        [StringLength(255, MinimumLength = 3)]
        public string Username { get; set; }
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

    }
}
