using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class Review
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public int Rate { get; set; }

        public long AuthorId { get; set; }

        public long RecipientId { get; set; }

        public virtual User Author { get; set; } = null!;

        public virtual User Recipient { get; set; } = null!;

        public Review(long id, string Description, int Rate, long AuthorId, long RecipientId, User Author, User Recipient)
        {
            Id = id;
            this.Description = Description;
            this.Rate = Rate;
            this.AuthorId = AuthorId;
            this.RecipientId = RecipientId;
            this.Author = Author;
            this.Recipient = Recipient;
        }
    }
}
