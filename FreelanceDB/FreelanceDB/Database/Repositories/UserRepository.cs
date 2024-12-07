using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Contracts;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FreelanceDB.Database.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly FreelancedbContext _context;
        public UserRepository(FreelancedbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUser(string login)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
            if (user == null) 
            { 
                return  false; 
            }
            else return true;
        }

        public async Task<long> Create(User user)
        {
           
          //обработать ошибку создания
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var us = await _context.Users.FirstAsync(l => user.Login == l.Login && user.PasswordHash == l.PasswordHash);
            return us.Id;

        }

        public async Task<bool> Delete(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel> Get(string login)
        {
            User? user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(r => r.Login == login);

            if (user == null)
            {
                var us = new UserModel();//если вернуло пустого юзера то в контроллере обработать
                return us;
            }
            else
            {
                UserModel user1 = Converter(user);
                user1.RoleName = user.Role.Role1;
                return user1;
            }

        }

        public async Task<UserModel> Get(long id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                var us = new UserModel();//если вернуло пустого юзера то в контроллере обработать
                return us;
            }
            else
            {
                UserModel user1 = Converter(user);
                return user1;
            }
        }

        public async Task<long> AddTokens(long id, string RToken, string AToken, DateTime expiry)
        {
            User user1 = await _context.Users.FindAsync(id);
            user1.AToken = AToken;
            user1.RToken = RToken;
            user1.RefreshTokenExpiryTime = expiry;
            await _context.SaveChangesAsync();
            return id;
            
        }

        public async Task<long> ChangeRole(long id, long role)//TODO вынести в контроллер
        {
            User user = await _context.Users.FindAsync(id);
            user.RoleId = role;
            _context.SaveChanges();
            return user.Id;
        }

        public async Task<(string, DateTime)> GetRTokenAndExpiryTime(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return (user.RToken, user.RefreshTokenExpiryTime);
        }

        public async Task<long> RemoveTokens(long id)
        {
            User user = await _context.Users.FindAsync(id);
            user.AToken= null;
            user.RToken = null;
            user.RefreshTokenExpiryTime = default;
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<long> UpdateUser(UserModel user)
        {
            User updateUser = Converter(user);
            await _context.SaveChangesAsync(); 
            return updateUser.Id;
        }

        private UserModel Converter(User user)//методы конвертирующие из User в UserModel и наоборот
        {
            return new UserModel(user.Id, user.Login, user.PasswordHash, user.Nickname, user.AToken, user.RToken, user.Balance, user.FreezeBalance, user.RoleId, user.RefreshTokenExpiryTime, user.Salt);
        }


        private User Converter(UserModel model)
        {
            return new User
            {
                Id = model.Id,
                Login = model.Login,
                PasswordHash = model.PasswordHash,
                Nickname = model.Nickname,
                AToken = model.AToken,
                RToken = model.RToken,
                RefreshTokenExpiryTime = model.RefreshTokenExpiryTime,
                RoleId = model.RoleId,
                Balance = model.Balance,
                FreezeBalance = model.FreezeBalance
            };
        }
    }
}
