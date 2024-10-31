namespace FreelanceDB.Models
{
    public class UserResume
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long ResumeId { get; set; }

        public virtual Resume Resume { get; set; } = null!;

        public virtual User User { get; set; } = null!;

        public UserResume(long id, long userId, long resumeId, Resume resume, User user)
        {
            Id = id;
            UserId = userId;
            ResumeId = resumeId;
            Resume = resume;
            User = user;
        }
    }
}
