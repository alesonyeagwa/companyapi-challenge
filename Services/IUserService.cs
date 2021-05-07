using CompanyAPI.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        User GetUser(int userId);
        Task<User> AddUser(User user);
        bool UserExists(int id);
        bool UsernameExists(string username);
    }
}
