using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User?> CreateUser(User user);
        Task<User?> UpdateUser(int id, User user);
        Task<User> DeleteUser(int id);
    }
}
