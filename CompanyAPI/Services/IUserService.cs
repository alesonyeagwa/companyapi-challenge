using CompanyAPI.Models.Requests;
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
        User GetUser(long userId);
        Task<User> AddUser(RegisterRequest userReq);
        bool UserExists(long id);
        bool UsernameExists(string username);
    }
}
