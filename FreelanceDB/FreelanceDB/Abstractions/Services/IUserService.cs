using FreelanceDB.Contracts.Requests;
using FreelanceDB.Database.Repositories;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> ChekUser(string login);

        Task<long> CreateUser(SignUpRequest user);

        Task<bool> DeleteUser(long id);

        Task<UserModel> GetUser(string login, string passwordhash);

        Task<UserModel> GetUser(long id);

        Task<(string, DateTime)> GetRTokenAndExpiryTime(long id);

        Task<long> RemoveTokens(long id);

        Task<long> UpdateUser(UserModel user);

        Task<long> UpdateUsersAToken(long id, string atoken);

    }
}
