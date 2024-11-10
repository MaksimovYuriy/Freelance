using FreelanceDB.Database.Entities;


namespace FreelanceDB.Models
{
    public class ResponseModel
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long UserId { get; set; }

        public ResponseModel(long id, long TaskId, long UserId)
        {
            Id = id;
            this.TaskId = TaskId;
            this.UserId = UserId;
        }

        public ResponseModel() { }
    }
}
