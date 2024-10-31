using FreelanceDB.Database.Entities;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions
{
    public interface IUserRepository
    {
        Task<UserModel> Get(string login, string passwordhash);
        Task<long> Create(UserModel user);
        Task<bool> CheckUser(string login);
        Task<bool> Delete(User user);    
    }
}
