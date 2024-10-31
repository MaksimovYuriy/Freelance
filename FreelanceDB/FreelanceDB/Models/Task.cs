using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class Task
    {
        public long Id { get; set; }

        public string Head { get; set; } = null!;

        public DateOnly Deadline { get; set; }

        public DateOnly? EndDate { get; set; }

        public int Price { get; set; }

        public string Description { get; set; } = null!;

        public string Tag { get; set; } = null!;

        public long AuthorId { get; set; }

        public long? ExecutorId { get; set; }

        public int StatusId { get; set; }

        public virtual User Author { get; set; } = null!;

        public virtual User? Executor { get; set; }

        public virtual Status Status { get; set; } = null!;

        public Task(long id,string Head, DateOnly Deadline, DateOnly? EndDate, int Price, string Description, string Tag, long AuthorId, long? ExecutorId, int StatusId, User Author, User? Executor, Status Status)
        {
            Id = id;
            this.Head = Head;
            this.Deadline = Deadline;
            this.EndDate = EndDate;
            this.Price = Price;
            this.Description = Description;
            this.Tag = Tag;
            this.AuthorId = AuthorId;
            this.ExecutorId = ExecutorId;
            this.StatusId = StatusId;
            this.Author = Author;
            this.Executor = Executor;
            this.Status = Status;

        }
    }
}
