using FreelanceDB.Models;

namespace FreelanceDB.Contracts.Requests.TaskResponse
{
    public record TasksResponse
    (
        List<TaskModel> tasks
    );
}
