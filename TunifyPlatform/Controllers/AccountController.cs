using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models; // For RegisterDto and LoginDto
using TunifyPlatform.Repositories.Interfaces; // For IAccount interface
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TunifyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(IAccount accountService, UserManager<IdentityUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
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

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null && await _accountService.LoginUser(loginDto))
            {
                var token = await _accountService.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Login failed");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUser();
            return Ok("Logged out successfully");
        }
    }
}
