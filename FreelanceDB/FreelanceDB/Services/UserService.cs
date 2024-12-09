using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Authentication.Abstractions;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository repository, ITokenService service, IPasswordHasher hasher)
        {
            _userRepository = repository;
            _tokenService = service;
            _passwordHasher = hasher;
        }

        public async Task<bool> ChekUser(string login)
        {
            return await _userRepository.CheckUser(login);
        }

        public async Task<long> CreateUser(SignUpRequest user)
        {
            //хэширование пароля
            if (await _userRepository.CheckUser(user.Login))
            {
                return 0;
            }
            else
            {
                string hash = _passwordHasher.HashPassword(user.password, out byte[] salt);

                User user1 = new User
                {
                    Login = user.Login,
                    PasswordHash = hash,
                    Salt = salt,
                    Nickname = user.Name,
                    RoleId = 1
                };
                long id = await _userRepository.Create(user1);
                var rtoken = _tokenService.GenerateRefreshToken();
                var atoken = _tokenService.GenerateAccessToken(id, user1.RoleId.ToString());
                var time = _tokenService.GetRefreshTokenExpireTime();
                await _userRepository.AddTokens(id, rtoken, atoken, time);

                return id;
            }
        }

        public async Task<bool> DeleteUser(long id)
        {

            return await _userRepository.Delete(id);
        }

        public async Task<UserModel> GetUser(string login, string password)//login
        {
            //хэширование пароля
            
            UserModel user = await _userRepository.Get(login);
            if (user == null)//если логин неверен
            {
                return new UserModel();
            }
            if (_passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt))//если логин и пароль верны
            {
                var atoken = _tokenService.GenerateAccessToken(user.Id, user.RoleName);
                var rtoken = _tokenService.GenerateRefreshToken();
                var time = _tokenService.GetRefreshTokenExpireTime();
                user.AToken = atoken;
                user.RToken = rtoken;
                _userRepository.AddTokens(user.Id, rtoken,atoken , time );
                return user;
            }
            else//если пароль неверен
            {
                return new UserModel();
            }
            
        }

        public async Task<(string, DateTime, string)> GetRTokenAndExpiryTimeAndRole(long id)
        {
            var data = await _userRepository.GetRTokenAndExpiryTimeAndRole(id);
            return data;
        }

        public async Task<UserModel> GetUser(long id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<long> RemoveTokens(long id)
        {
            await _userRepository.RemoveTokens(id);
            return(id);
        }

        public async Task<long> UpdateUser(UserModel user)
        {
            await _userRepository.UpdateUser(user);
            return user.Id;
        }

        public async Task<long> UpdateUsersAToken(long id, string atoken)
        {
            var user = await _userRepository.Get(id);
            user.AToken = atoken;
            await _userRepository.UpdateUser(user);
            return id;
        }
    }
}
