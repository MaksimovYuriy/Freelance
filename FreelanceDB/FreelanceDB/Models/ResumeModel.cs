using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class ResumeModel
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long UserId { get; set; }


        public ResumeModel(long id, long taskId, long userId)
        {
            Id = id;
            TaskId = taskId;
            UserId = userId;
        }
    }
}
