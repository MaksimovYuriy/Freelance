using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class TaskModel
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

        public TaskModel(long id, string Head, DateOnly Deadline, DateOnly? EndDate, int Price, string Description, string Tag, long AuthorId, int StatusId)
        {
            Id = id;
            this.Head = Head;
            this.Deadline = Deadline;
            this.EndDate = EndDate;
            this.Price = Price;
            this.Description = Description;
            this.Tag = Tag;
            this.AuthorId = AuthorId;
            this.StatusId = StatusId;
        }

        public TaskModel() { }
    }
}
