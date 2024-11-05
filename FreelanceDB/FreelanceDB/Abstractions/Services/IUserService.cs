using FreelanceDB.Database.Repositories;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> ChekUser(string login);

        Task<long> CreateUser(UserModel user);

        Task<bool> DeleteUser(long id);

        Task<UserModel> GetUser(string login, string passwordhash);

        Task<UserModel> GetUser(long id);
    }
}
