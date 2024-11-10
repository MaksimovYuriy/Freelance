namespace FreelanceDB.Database.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
