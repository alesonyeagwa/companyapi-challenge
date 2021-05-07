using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string password { get; set; }
    }
}
