using FreelanceDB.Database.Entities;

namespace FreelanceDB.Models
{
    public class ReviewModel
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public int Rate { get; set; }

        public long AuthorId { get; set; }

        public long RecipientId { get; set; }

        public ReviewModel(long id, string Description, int Rate, long AuthorId, long RecipientId)
        {
            Id = id;
            this.Description = Description;
            this.Rate = Rate;
            this.AuthorId = AuthorId;
            this.RecipientId = RecipientId;
        }
    }
}
