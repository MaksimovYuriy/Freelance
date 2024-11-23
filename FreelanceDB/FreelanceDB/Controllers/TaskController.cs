using FreelanceDB.Abstractions.Services;
using FreelanceDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasks();
            return Ok(result);
        }

        [HttpGet("GetTasksAuthor")]
        public async Task<IActionResult> GetTasksAuthor(long userId)
        {
            var result = await _taskService.GetTasksAuthor(userId);
            return Ok(result);
        }

        [HttpGet("GetTasksExecutor")]
        public async Task<IActionResult> GetTasksExecutor(long userId)
        {
            var result = await _taskService.GetTasksExecutor(userId);
            return Ok(result);
        }

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById(long taskId)
        {
            var result = await _taskService.GetTaskById(taskId);
            return Ok(result);
        }

        [HttpPut("DeleteTaskExecutor")]
        public async Task<IActionResult> DeleteTaskExecutor(long taskId)
        {
            var result = await _taskService.DeleteTaskExecutor(taskId);
            return Ok(result);
        }

        [HttpPut("AddTaskExecutor")]
        public async Task<IActionResult> AddTaskExecutor(long taskId, long executorId)
        {
            var result = await _taskService.AddTaskExecutor(taskId, executorId);
            return Ok(result);
        }
    }
}
