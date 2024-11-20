using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Role
{
    public long Id { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
