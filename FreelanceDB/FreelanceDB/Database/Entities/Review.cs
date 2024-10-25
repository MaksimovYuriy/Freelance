using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Review
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public int Rate { get; set; }

    public long AuthorId { get; set; }

    public long RecipientId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual User Recipient { get; set; } = null!;
}
