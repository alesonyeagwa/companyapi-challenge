using CompanyAPI.Data;
using CompanyAPI.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly CompanyAPIContext _context;

        public UserService(CompanyAPIContext aPIContext)
        {
            this._context = aPIContext;
        }
        public User GetUser(int userId)
        {
            return  _context.Users.FirstOrDefault(user => user.Id == userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
