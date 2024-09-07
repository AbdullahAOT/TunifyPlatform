using Microsoft.AspNetCore.Identity;
using TunifyPlatform.Repositories.Interfaces;
using TunifyPlatform.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TunifyPlatform.Repositories.Services
{
    public class UserService : IUsers
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> CreateUser(RegisterDto registerDto)
        {
            var user = new IdentityUser { UserName = registerDto.Email, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return user;
            }
            return null; // Handle errors appropriately
        }

        public async Task<IdentityUser> UpdateUser(string id, RegisterDto registerDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            user.Email = registerDto.Email;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? user : null;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
