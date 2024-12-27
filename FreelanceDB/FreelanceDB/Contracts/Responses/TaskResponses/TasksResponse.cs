using FreelanceDB.Models;

namespace FreelanceDB.Contracts.Responses.TaskResponses
{
    public record TasksResponse
    (
        List<TaskModel> tasks
    );
}
