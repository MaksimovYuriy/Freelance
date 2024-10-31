using FreelanceDB.Abstractions;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FreelanceDB.Database.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly FreelanceDbContext _context;
        public UserRepository(FreelanceDbContext context)
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

        public async Task<long> Create(UserModel user)
        {
            string atoken = "atoken";//заглушка пока не готова автормзация
            string rtoken = "rtoken";
            User newuser = new User(user.Login, user.PasswordHash, user.Nickname, atoken, rtoken);
            await _context.Users.AddAsync(newuser);
            await _context.SaveChangesAsync();

           var us = await _context.Users.FirstAsync(l => user.Login == l.Login && user.PasswordHash == l.PasswordHash);
            return us.Id;

        }

        public async Task<bool> Delete(User user)
        {
            if(user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel> Get(string login, string passwordhash)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(r => r.Login == login && r.PasswordHash == passwordhash);
            if (user == null)
            {
                var us = new UserModel();//если вернуло пустого юзера то в контроллере обработать
                return us;
            }
            else
            {
                UserModel user1 = new UserModel(user.Id, user.Login, user.PasswordHash, user.Nickname, user.AToken, user.RToken, user.Balance, user.FreezeBalance);
                return user1;
            }

        }
    }
}
