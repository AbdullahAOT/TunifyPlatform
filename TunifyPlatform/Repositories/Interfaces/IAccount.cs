using TunifyPlatform.Models;
using System.Threading.Tasks;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IAccount
    {
        Task<bool> RegisterUser(RegisterDto registerDto);
        Task<bool> LoginUser(LoginDto loginDto);
        Task LogoutUser();
    }
}
