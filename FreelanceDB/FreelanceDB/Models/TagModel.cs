namespace FreelanceDB.Models
{
    public class TagModel
    {
        public long Id { get; set; }
        public string TagName { get; set; } = null!;

        public TagModel(long id, string name)
        {
            this.Id = id;
            this.TagName = name;
        }

        public TagModel() { }
    }
}
