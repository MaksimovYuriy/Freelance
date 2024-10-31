namespace FreelanceDB.Models
{
    public class StatusModel
    {
        public int Id { get; set; }

        public string StatusName { get; set; } = null!;


        public StatusModel(int id, string statusName)
        {
            Id = id;
            StatusName = statusName;
        }

    }
}
