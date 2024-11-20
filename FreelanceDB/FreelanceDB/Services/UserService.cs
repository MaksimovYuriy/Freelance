using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Authentication;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository repository, ITokenService service)
        {
            _userRepository = repository;
            _tokenService = service;
        }

        public async Task<bool> ChekUser(string login)
        {
            return await _userRepository.CheckUser(login);
        }

        public async Task<long> CreateUser(SignUpRequest user)
        {
            //хэширование пароля
            User user1 = new User(user.Login, user.password, user.Name, 1);
            long id = await _userRepository.Create(user1);
            await _userRepository.AddTokens(id, _tokenService.GenerateRefreshToken(), _tokenService.GenerateAccessToken(id, "user"), _tokenService.GetRefreshTokenExpireTime());//заменить на айди ролей когда сделаю роли

            return id;
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
