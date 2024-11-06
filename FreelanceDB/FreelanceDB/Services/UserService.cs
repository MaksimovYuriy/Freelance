using FreelanceDB.Abstractions;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts;
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

        public async Task<long> CreateUser(UserRequest user)
        {
            //создание токенов
            //хэширование пароля
            User user1 = new User(user.Login, user.password, user.Name);
            return await _userRepository.Create(user1);
        }

        public async Task<bool> DeleteUser(long id)
        {

            return await _userRepository.Delete(id);
        }

        public async Task<UserModel> GetUser(string login, string passwordhash)
        {
            //хэширование пароля
            return await _userRepository.Get(login, passwordhash);
        }

        public async Task<UserModel> GetUser(long id)
        {
            return await _userRepository.Get(id);
        }

        public string GetPasHash(string password)
        {
            string hash = "hash";
            return hash;
        }
    }
}
