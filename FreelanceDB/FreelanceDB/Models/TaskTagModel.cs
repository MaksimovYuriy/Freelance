namespace FreelanceDB.Models
{
    public class TaskTagModel
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public long TagId { get; set; }

        public TaskTagModel(long id, long taskId, long tagId)
        {
            Id = id;
            TaskId = taskId;
            TagId = tagId;
        }

        public TaskTagModel() { }
    }
}
