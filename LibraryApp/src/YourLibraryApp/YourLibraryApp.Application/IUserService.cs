using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourLibraryApp.Domain;

namespace YourLibraryApp.Application
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(string username, string email, string password);
        Task<User> GetByIdAsync(int id);
        Task<bool> IsUsernameUniqueAsync(string username);
    }
}