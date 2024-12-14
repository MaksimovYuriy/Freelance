using FreelanceDB.Contracts.Requests;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetAllTasks();
        Task<TaskModel> GetTaskById(long taskId);
        Task<List<TaskModel>> GetFilteredTasks(FilterTasksRequest filter);
        Task<List<TaskModel>> GetTasksAuthor(long userId);
        Task<List<TaskModel>> GetTasksExecutor(long userId);
        Task<long> AddTaskExecutor(long taskId, long userId);
        Task<long> DeleteTaskExecutor(long taskId);
        Task<long> CreateTask(NewTaskRequest newTask);
        Task<long> CompleteTask(long taskId);
    }
}
