using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class UserResume
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ResumeId { get; set; }

    public virtual Resume Resume { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
