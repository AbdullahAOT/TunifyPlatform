using Microsoft.AspNetCore.Identity;
using TunifyPlatform.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IUsers
    {
        Task<IdentityUser> CreateUser(RegisterDto registerDto);
        Task<IdentityUser> UpdateUser(string id, RegisterDto registerDto);
        Task DeleteUser(string id);
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityUser> GetUserById(string id);
    }
}
