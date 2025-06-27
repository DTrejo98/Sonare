using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserByUid(string uid)
        {
            return await _userRepository.GetUserByUid(uid);
        }

        public async Task<User?> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            return await _userRepository.UpdateUser(id, user);
        }

        public async Task<User> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
