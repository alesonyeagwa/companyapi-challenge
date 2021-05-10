using CompanyAPI.Models.Schemas;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
        {
            public RegisterRequestValidator()
            {
                RuleFor(o => o.Username).NotEmpty().MinimumLength(3).MaximumLength(255);
                RuleFor(o => o.Password).NotEmpty().MinimumLength(3).MaximumLength(50);
            }
        }
    }
}
