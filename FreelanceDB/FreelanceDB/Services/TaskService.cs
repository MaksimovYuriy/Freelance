using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Database.Repositories;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IResponseRepository _responseRepository;
        private readonly ITagRepository _tagRepository;

        public TaskService(ITaskRepository taskRepository, 
            IResponseRepository responseRepository, ITagRepository tagRepository)
        {
            _taskRepository = taskRepository;
            _responseRepository = responseRepository;
            _tagRepository = tagRepository;
        }

        public async Task<long> AddTaskExecutor(long taskId, long userId)
        {
            long status = await _taskRepository.AddExecutor(taskId, userId);
            return status;
        }

        public async Task<long> CompleteTask(long taskId)
        {
            var status = await _taskRepository.ChangeStatus(taskId, 2);
            if(status != 0)
            {
                status = await _taskRepository.SetEndDate(taskId);
                return status;
            }
            else
            {
                return 0;
            }
        }

        public async Task<long> CreateTask(NewTaskRequest newTask)
        {
            TaskModel model = new TaskModel();
            model.Head = newTask.Head;
            model.Deadline = newTask.Deadline;
            model.Price = newTask.Price;
            model.Description = newTask.Description;
            model.AuthorId = newTask.AuthorId;
            model.StatusId = newTask.StatusId;

            var taskId = await _taskRepository.CreateTask(model);
            return taskId;
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
        
        // Объяснение что тут происходит.
        // Сначала сравниваем по заголовку.
        // Далее получаем список из всех задач, которые содержат любой из указанных тэгов.
        public async Task<List<TaskModel>> GetFilteredTasks(FilterTasksRequest filter)
        {
            var allTasks = await _taskRepository.GetAllTasks();
            if(filter.Head != null)
            {
                allTasks = allTasks.Where(p => p.Head.Contains(filter.Head)).ToList();
            }
            if(filter.Tags == null)
            {
                return allTasks;
            }
            List<TaskModel> filtered = new List<TaskModel>();
            foreach (var task in allTasks)
            {
                var taskTags = await _tagRepository.GetAllTags(task.Id);
                foreach(var needTagName in filter.Tags)
                {
                    TagModel? targetTag = taskTags.FirstOrDefault(p => p.TagName == needTagName);
                    if(targetTag != null)
                    {
                        filtered.Add(task);
                        break;
                    }
                }
            }
            return filtered;
        }

        public async Task<TaskModel> GetTaskById(long taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            return task;
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
