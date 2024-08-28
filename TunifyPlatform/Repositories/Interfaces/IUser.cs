using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IUsers
    {
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
    }
}
