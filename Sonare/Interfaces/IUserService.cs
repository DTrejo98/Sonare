using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserByUid(string uid);
        Task<User?> CreateUser(User user);
        Task<User?> UpdateUser(int id, User user);
        Task<User> DeleteUser(int id);
    }
}
