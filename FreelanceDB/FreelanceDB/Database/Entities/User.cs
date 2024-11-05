using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string? AToken { get; set; }

    public string? RToken { get; set; }

    public int Balance { get; set; }

    public int FreezeBalance { get; set; }

    public virtual ICollection<Review> ReviewAuthors { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewRecipients { get; set; } = new List<Review>();

    public virtual ICollection<Task> TaskAuthors { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskExecutors { get; set; } = new List<Task>();


    internal User(string login, string passwordhash, string nickname, string ? atoken, string ? rtoken, int balance=0, int freeze=0, long id = 0)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordhash;
        Nickname = nickname;
        AToken = atoken;
        RToken = rtoken;
        Balance = balance;
        FreezeBalance = freeze;
    }
    
    internal User()
    {

    }
}
