using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Database.Repositories;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IResponseRepository _responseRepository;

        public TaskService(ITaskRepository taskRepository, IResponseRepository responseRepository)
        {
            _taskRepository = taskRepository;
            _responseRepository = responseRepository;
        }

        public async Task<long> AddTaskExecutor(long taskId, long userId)
        {
            long status = await _taskRepository.AddExecutor(taskId, userId);
            return status;
        }

        public Task<long> CreateTask(TaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateTaskResponse(long taskId, long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<long> DeleteTaskExecutor(long taskId)
        {
            long status = await _taskRepository.DeleteExecutor(taskId);
            return status;
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return tasks;
        }

        public Task<List<TaskModel>> GetFilteredTasks(string? head, List<TagModel>? tags)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResponseModel>> GetMyResposes(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskModel> GetTaskById(long taskId)
        {
            var task = _taskRepository.GetTaskById(taskId);
            return task;
        }

        public Task<List<ResponseModel>> GetTaskResponses(long taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskModel>> GetTasksAuthor(long userId)
        {
            var tasks = await _taskRepository.GetAllTasks();
            tasks = tasks.Where(p => p.AuthorId == userId).ToList();
            return tasks;
        }

        public async Task<List<TaskModel>> GetTasksExecutor(long userId)
        {
            var tasks = await _taskRepository.GetAllTasks();
            tasks = tasks.Where(p => p.ExecutorId == userId).ToList();
            return tasks;
        }
    }
}
