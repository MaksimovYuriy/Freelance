namespace FreelanceDB.Contracts.Requests.TaskRequests
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
