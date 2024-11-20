using System;
using System.Collections.Generic;

namespace FreelanceDB.Database.Entities;

public partial class Task
{
    public long Id { get; set; }

    public string Head { get; set; } = null!;

    public DateOnly Deadline { get; set; }

    public DateOnly? EndDate { get; set; }

    public int Price { get; set; }

    public string Description { get; set; } = null!;

    public long AuthorId { get; set; }

    public long? ExecutorId { get; set; }

    public int StatusId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual User? Executor { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
