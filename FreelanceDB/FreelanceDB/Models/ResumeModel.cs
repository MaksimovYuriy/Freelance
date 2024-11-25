using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class ResumeModel
    {
        public long Id { get; set; }

        public string Head { get; set; } = null!;

        public string? WorkExp { get; set; }

        public string? Skills { get; set; }

        public string? Education { get; set; }

        public string? AboutMe { get; set; }

        public string? Contacts { get; set; }

        public long UserId { get; set; }

        public ResumeModel(long id, string head, string? workExp, string? skills, string? education, string? aboutMe, string? contacts)
        {
            this.Id = id;
            this.Head = head;
            this.WorkExp = workExp;
            this.Skills = skills;
            this.Education = education;
            this.AboutMe = aboutMe;
            this.Contacts = contacts;
        }

        public ResumeModel() { }
    }
}
