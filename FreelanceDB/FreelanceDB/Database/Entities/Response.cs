using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Response
{
    public long Id { get; set; }

    public long TaskId { get; set; }

    public long UserId { get; set; }

    public DateTime ResponseDate { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
