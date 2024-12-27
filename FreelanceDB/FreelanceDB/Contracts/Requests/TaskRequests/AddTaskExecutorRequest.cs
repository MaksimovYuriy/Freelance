namespace FreelanceDB.Contracts.Requests.TaskRequests
{
    public record AddTaskExecutorRequest
    (
        long taskId,
        long executorId
    );
}
