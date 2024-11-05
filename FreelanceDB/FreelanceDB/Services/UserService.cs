using FreelanceDB.Abstractions;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<bool> ChekUser(string login)
        {
            return await _userRepository.CheckUser(login);
        }

        public async Task<long> CreateUser(UserModel user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<bool> DeleteUser(long id)
        {

            return await (_userRepository.Delete(id));
        }

        public async Task<UserModel> GetUser(string login, string passwordhash)
        {
            return await GetUser(login, passwordhash);
        }

        public async Task<UserModel> GetUser(long id)
        {
            return await GetUser(id);
        }
    }
}
