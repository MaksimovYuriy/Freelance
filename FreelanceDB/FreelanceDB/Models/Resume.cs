using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class Resume
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long UserId { get; set; }

        public virtual Database.Entities.Task Task { get; set; } = null!;

        public virtual User User { get; set; } = null!;

        public Resume(long id, long taskId, long userId, Database.Entities.Task task, User user)
        {
            Id = id;
            TaskId = taskId;
            UserId = userId;
            Task = task;
            User = user;
        }
    }
}
