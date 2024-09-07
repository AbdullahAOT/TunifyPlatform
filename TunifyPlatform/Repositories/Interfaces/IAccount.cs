using TunifyPlatform.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IAccount
    {
        Task<bool> RegisterUser(RegisterDto registerDto);
        Task<bool> LoginUser(LoginDto loginDto);
        Task LogoutUser();
        Task<string> GenerateJwtToken(IdentityUser user);
    }
}
