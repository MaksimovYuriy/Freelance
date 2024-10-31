namespace FreelanceDB.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string StatusName { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

        public Status(int id, string statusName, ICollection<Task> tasks)
        {
            Id = id;
            StatusName = statusName;
            Tasks = tasks;
        }

    }
}
