using CompanyAPI.Data;
using CompanyAPI.Models.Requests;
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
        public User GetUser(long userId)
        {
            return  _context.Users.FirstOrDefault(user => user.Id == userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> AddUser(RegisterRequest userReq)
        {
            User user = new User()
            {
                UserName = userReq.Username,
                Password = userReq.Password
            };

            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.UserName == username);
        }
    }
}
