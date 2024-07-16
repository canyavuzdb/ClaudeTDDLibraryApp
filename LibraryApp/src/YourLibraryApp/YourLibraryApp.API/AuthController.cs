using Microsoft.AspNetCore.Mvc;
using YourLibraryApp.Application;
using YourLibraryApp.API.Models;
using YourLibraryApp.API.Helpers;

namespace YourLibraryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtUtils _jwtUtils;

        public AuthController(IUserService userService, JwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var token = _jwtUtils.GenerateJwtToken(user);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Token = token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!await _userService.IsUsernameUniqueAsync(model.Username))
                return BadRequest(new { message = "Username is already taken" });

            var user = await _userService.RegisterAsync(model.Username, model.Email, model.Password);

            return Ok(new { message = "Registration successful" });
        }
    }
}