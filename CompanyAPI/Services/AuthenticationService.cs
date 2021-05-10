using CompanyAPI.Data;
using CompanyAPI.Helpers;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Responses;
using CompanyAPI.Models.Schemas;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Repositories
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CompanyAPIContext _context;
        private readonly AppSettings _appSettings;
        public AuthenticationService(CompanyAPIContext aPIContext, IOptions<AppSettings> settings)
        {
            this._context = aPIContext;
            this._appSettings = settings.Value;
        }
        public LoginResponse Authenticsate(LoginRequest request)
        {
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            User user = _context.Users.FirstOrDefault(u => u.UserName == request.username);
            if (user == null)
            {
                return null;
            }
            bool verified = BCrypt.Net.BCrypt.Verify(request.password, user.Password);
            if (!verified)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, request.username)
                    }
                 ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponse(user, tokenHandler.WriteToken(token));
        }
    }
}
