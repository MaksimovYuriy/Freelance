using FreelanceDB.Abstractions.Services;
using FreelanceDB.Database.Repositories;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _taskRepository;
        private readonly ResponseRepository _responseRepository;

        public TaskService(TaskRepository taskRepository, ResponseRepository responseRepository)
        {
            _taskRepository = taskRepository;
            _responseRepository = responseRepository;
        }

        public Task<long> AddTaskExecutor(long taskId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateTask(TaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateTaskResponse(long taskId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<long> DeleteTaskExecutor(long taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetFilteredTasks(string? head, List<TagModel>? tags)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResponseModel>> GetMyResposes(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResponseModel>> GetTaskResponses(long taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetTasksAuthor(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetTasksExecutor(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
