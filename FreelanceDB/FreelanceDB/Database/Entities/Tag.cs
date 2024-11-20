using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Tag
{
    public long Id { get; set; }

    public string Tag1 { get; set; } = null!;

    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
