using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Database.Context;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly FreelancedbContext _context;

        public TaskRepository(FreelancedbContext context) {
            _context = context;
        }

        public async Task<long> AddExecutor(long taskId, long executorId)
        {
            await _context.Tasks.Where(task => task.Id == taskId && task.ExecutorId == null)
                .ExecuteUpdateAsync(task => task.SetProperty(m => m.ExecutorId, m => executorId));

            return executorId;
        }

        public async Task<long> CreateTask(TaskModel task)
        {
            Entities.Task entity = new Entities.Task()
            {
                Head = task.Head,
                Deadline = task.Deadline,
                EndDate = task.EndDate,
                Price = task.Price,
                Description = task.Description,
                AuthorId = task.AuthorId,
                ExecutorId = task.ExecutorId,
                StatusId = task.StatusId
            };

            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<long> DeleteTask(long id)
        {
            var status = await _context.Tasks.Where(task => task.Id == id).ExecuteDeleteAsync();

            if(status == 0)
            {
                throw new Exception("Unknown task.id");
            }

            return id;
        }

        public async Task<List<TaskModel>> GetAllTasksByAuthor(long authorId)
        {
            var tasksEntity = await _context.Tasks.Where(task => task.AuthorId == authorId).ToListAsync();

            if(tasksEntity.Any())
            {
                throw new Exception("Unknown task.authorId");
            }

            List<TaskModel> models = new List<TaskModel>();

            foreach (var task in tasksEntity)
            {
                TaskModel model = new TaskModel()
                {
                    Id = task.Id,
                    Head = task.Head,
                    Deadline = task.Deadline,
                    EndDate = task.EndDate,
                    Price = task.Price,
                    Description = task.Description,
                    AuthorId = task.AuthorId,
                    ExecutorId = task.ExecutorId,
                    StatusId = task.StatusId
                };

                models.Add(model);
            }

            return models;
        }

        public async Task<List<TaskModel>> GetAllTasksByExecutor(long executorId)
        {
            var tasksEntity = await _context.Tasks.Where(task => task.ExecutorId == executorId).ToListAsync();

            if (tasksEntity.Any())
            {
                throw new Exception("Unknown task.executorId");
            }

            List<TaskModel> models = new List<TaskModel>();

            foreach (var task in tasksEntity)
            {
                TaskModel model = new TaskModel()
                {
                    Id = task.Id,
                    Head = task.Head,
                    Deadline = task.Deadline,
                    EndDate = task.EndDate,
                    Price = task.Price,
                    Description = task.Description,
                    AuthorId = task.AuthorId,
                    ExecutorId = task.ExecutorId,
                    StatusId = task.StatusId
                };

                models.Add(model);
            }

            return models;
        }

        public async Task<TaskModel> GetTaskById(long id)
        {
            var taskEntity = await _context.Tasks.Where(task => task.Id == id).FirstOrDefaultAsync();

            if (taskEntity == null)
            {
                throw new Exception("Unknown task.id");
            }

            TaskModel model = new TaskModel()
            {
                Id = id,
                Head = taskEntity.Head,
                Deadline = taskEntity.Deadline,
                EndDate = taskEntity.EndDate,
                Price = taskEntity.Price,
                Description = taskEntity.Description,
                AuthorId = taskEntity.AuthorId,
                ExecutorId = taskEntity.ExecutorId,
                StatusId = taskEntity.StatusId
            };

            return model;
        }

        public async Task<long> UpdateTask(long id, TaskModel task)
        {
            await _context.Tasks.Where(task => task.Id == id)
                .ExecuteUpdateAsync(p => p
                .SetProperty(m => m.Head, m => task.Head)
                .SetProperty(m => m.Deadline, m => task.Deadline)
                .SetProperty(m => m.EndDate, m => task.EndDate)
                .SetProperty(m => m.Price, m => task.Price)
                .SetProperty(m => m.Description, m => task.Description)
                .SetProperty(m => m.AuthorId, m => task.AuthorId)
                .SetProperty(m => m.ExecutorId, m => task.ExecutorId)
                .SetProperty(m => m.StatusId, m => task.StatusId));

            return id;
        }
    }
}
