using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _userService;

        public UsersController(IUsers userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUser>> GetUser(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityUser>> PostUser(RegisterDto registerDto)
        {
            var createdUser = await _userService.CreateUser(registerDto);
            return createdUser != null ? CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser) : BadRequest("User creation failed.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, RegisterDto registerDto)
        {
            var updatedUser = await _userService.UpdateUser(id, registerDto);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
