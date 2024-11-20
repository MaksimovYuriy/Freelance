using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetAllTasks();
        Task<long> CreateTask(TaskModel model);
        Task<long> GetTaskResponses(long taskId);
        Task<long> CreateTaskResponse(long taskId, long userId);
    }
}
