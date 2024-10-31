using FreelanceDB.Database.Entities;


namespace FreelanceDB.Models
{
    public class Response
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long UserId { get; set; }

        public virtual Database.Entities.Task Task { get; set; } = null!;

        public virtual User User { get; set; } = null!;

        public Response(long id, long TaskId, long UserId, Database.Entities.Task Task, User User)
        {
            Id = id;
            this.TaskId = TaskId;
            this.UserId = UserId;
            this.Task = Task;
            this.User = User;
        }
    }

    
}
