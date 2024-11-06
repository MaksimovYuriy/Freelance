using FreelanceDB.Abstractions;
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
                UserModel user1 = Converter(user);
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

        private UserModel Converter(User user)//методы конвертирующие из User в UserModel и наоборот
        {
            return new UserModel(user.Id, user.Login, user.PasswordHash, user.Nickname, user.AToken, user.RToken, user.Balance, user.FreezeBalance);
        }
        private User Converter(UserModel model) 
        {
            return new User(model.Login, model.PasswordHash, model.Nickname, model.AToken, model.RToken, model.Balance, model.FreezeBalance, model.Id);
        }
    }
}
