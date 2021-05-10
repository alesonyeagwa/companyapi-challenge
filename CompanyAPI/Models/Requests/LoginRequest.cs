using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }

        public class LoginRequestValidator : AbstractValidator<LoginRequest>
        {
            public LoginRequestValidator()
            {
                RuleFor(o => o.username).NotEmpty().MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.password).NotEmpty().MinimumLength(3).MaximumLength(50);
            }
        }
    }
}
