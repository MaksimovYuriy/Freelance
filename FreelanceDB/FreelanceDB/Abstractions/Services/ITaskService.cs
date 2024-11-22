using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetAllTasks();
        Task<List<TaskModel>> GetFilteredTasks(string? head, List<TagModel>? tags);
        Task<List<TaskModel>> GetTasksAuthor(long userId);
        Task<List<TaskModel>> GetTasksExecutor(long userId);
        Task<List<ResponseModel>> GetTaskResponses(long taskId);
        Task<long> AddTaskExecutor(long taskId, long userId);
        Task<long> DeleteTaskExecutor(long taskId);
        Task<long> CreateTask(TaskModel model);
        Task<long> CreateTaskResponse(long taskId, long userId);
        Task<List<ResponseModel>> GetMyResposes(long userId);
    }
}
