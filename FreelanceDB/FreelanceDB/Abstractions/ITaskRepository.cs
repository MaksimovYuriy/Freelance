﻿using FreelanceDB.Models;

namespace FreelanceDB.Abstractions
{
    public interface ITaskRepository
    {
        Task<TaskModel> GetTaskById(long id);
        Task<List<TaskModel>> GetAllTasksByAuthor(long authorId);
        Task<List<TaskModel>> GetAllTasksByExecutor(long executorId);
        Task<long> CreateTask(TaskModel task);
        Task<long> AddExecutor(long taksId, long executorId);
        Task<long> UpdateTask(long id, TaskModel task);
        Task<long> DeleteTask(long id);
    }
}
