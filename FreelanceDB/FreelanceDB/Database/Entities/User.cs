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

    public User(long id, string login, string passwordHash, string nickname, string? aToken, string? rToken, int balance, int freezeBalance, ICollection<Review> reviewAuthors, ICollection<Review> reviewRecipients, ICollection<Task> taskAuthors, ICollection<Task> taskExecutors)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
        Nickname = nickname;
        AToken = aToken;
        RToken = rToken;
        Balance = balance;
        FreezeBalance = freezeBalance;
        ReviewAuthors = reviewAuthors;
        ReviewRecipients = reviewRecipients;
        TaskAuthors = taskAuthors;
        TaskExecutors = taskExecutors;
    }
}
