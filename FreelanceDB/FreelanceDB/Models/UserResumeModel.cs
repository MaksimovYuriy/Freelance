namespace FreelanceDB.Models
{
    public class UserResumeModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long ResumeId { get; set; }

        public virtual ResumeModel Resume { get; set; } = null!;

        public virtual UserModel User { get; set; } = null!;

        public UserResumeModel(long id, long userId, long resumeId, ResumeModel resume, UserModel user)
        {
            Id = id;
            UserId = userId;
            ResumeId = resumeId;
            Resume = resume;
            User = user;
        }
    }
}
