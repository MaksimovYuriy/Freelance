namespace FreelanceDB.Contracts.Requests
{
    public record NewTaskRequest
    (
        string Head,
        DateOnly Deadline,
        int Price,
        string Description,
        long AuthorId,
        int StatusId
    );
}
