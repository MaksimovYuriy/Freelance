using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public byte[] Salt {  get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string? AToken { get; set; }

    public string? RToken { get; set; }

    public int Balance { get; set; }

    public int FreezeBalance { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public long RoleId { get; set; }

    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();

    public virtual ICollection<Review> ReviewAuthors { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewRecipients { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Task> TaskAuthors { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskExecutors { get; set; } = new List<Task>();
}
