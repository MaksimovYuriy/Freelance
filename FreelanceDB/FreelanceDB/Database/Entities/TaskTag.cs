using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class TaskTag
{
    public long Id { get; set; }

    public long TaskId { get; set; }

    public long TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
