using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Status
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
