using System;
using System.Threading.Tasks;
using YourLibraryApp.Domain;
using BCrypt.Net;

namespace YourLibraryApp.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                Role = "User" // Default role
            };

            return await _userRepository.CreateAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            return existingUser == null;
        }
    }
}