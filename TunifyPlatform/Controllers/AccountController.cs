using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models; // For RegisterDto and LoginDto
using TunifyPlatform.Repositories.Interfaces; // For IAccount interface
using System.Threading.Tasks;

namespace TunifyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;

        public AccountController(IAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _accountService.RegisterUser(registerDto);
            if (!result) return BadRequest("Registration failed");

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _accountService.LoginUser(loginDto);
            if (!result) return Unauthorized("Login failed");

            return Ok("Login successful");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUser();
            return Ok("Logged out successfully");
        }
    }
}
