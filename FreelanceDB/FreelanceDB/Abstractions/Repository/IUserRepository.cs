using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> Get(string login);
        Task<UserModel> Get(long id);
        Task<long> Create(User user);
        Task<bool> CheckUser(string login);
        Task<bool> Delete(long id);
        Task<long> AddTokens(long id, string RToken, string AToken, DateTime expiry);
        Task<(string, DateTime, string)> GetRTokenAndExpiryTimeAndRole(long id);
        Task<long> ChangeRole(long id, long role);
        Task<long> RemoveTokens(long id);
        Task<long> UpdateUser(UserModel user);
    }
}
