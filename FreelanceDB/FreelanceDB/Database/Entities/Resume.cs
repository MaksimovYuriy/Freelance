using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Resume
{
    public long Id { get; set; }

    public string Head { get; set; } = null!;

    public string? WorkExp { get; set; }

    public string? Skills { get; set; }

    public string? Education { get; set; }

    public string? AboutMe { get; set; }

    public string? Contacts { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
