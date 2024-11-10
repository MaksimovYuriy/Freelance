using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class User//TODO: роли, бан, время истечения срока действия токена
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public long RoleId { get; set; }   

    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? AToken { get; set; }

    public string? RToken { get; set; }

    public int Balance { get; set; }

    public int FreezeBalance { get; set; }

   // public virtual Role? Role { get; set; }

    public virtual ICollection<Review> ReviewAuthors { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewRecipients { get; set; } = new List<Review>();

    public virtual ICollection<Task> TaskAuthors { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskExecutors { get; set; } = new List<Task>();


    internal User(string login, string passwordhash ,string nickname, string ? atoken, string ? rtoken, DateTime refreshExpiry, int balance=0, int freeze=0, long id = 0, long role = 1)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordhash;
        Nickname = nickname;
        AToken = atoken;
        RToken = rtoken;
        Balance = balance;
        FreezeBalance = freeze;
        RoleId = role;
        RefreshTokenExpiryTime = refreshExpiry;
    }
    internal User(string login, string passwordhash, string nickname, long role)
    {
        Login = login; 
        PasswordHash = passwordhash;
        Nickname = nickname;
        RoleId = role;
    }
    internal User()
    {

    }
}
