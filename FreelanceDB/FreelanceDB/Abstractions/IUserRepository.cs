using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions
{
    public interface IUserRepository
    {
        Task<UserModel> Get(string login, string passwordhash);
        Task<UserModel> Get(long id);
        Task<long> Create(User user);
        Task<bool> CheckUser(string login);
        Task<bool> Delete(long id);
        Task<long> AddTokens(long id, string RToken, string AToken, DateTime expiry);
        Task<long> ChangeRole(long id, long role);
    }
}
