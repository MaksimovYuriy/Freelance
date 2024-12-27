using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Contracts.Requests.TaskRequests;
using FreelanceDB.Models;
using FreelanceDB.RabbitMQ;
using FreelanceDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;
        private readonly IRabbitMqService _rabbitMqService;
        public TaskController(ITaskService taskService, ILogger<TaskController> logger, IRabbitMqService rabbitMqService)
        {
            _taskService = taskService;
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            _logger.LogInformation("Get tasks " + DateTime.Now.ToString());
            var result = await _taskService.GetAllTasks();
            return Ok(result);
        }

        [HttpGet("GetTasksAuthor")]
        public async Task<IActionResult> GetTasksAuthor([FromQuery] TasksByAuthorIdRequest request)
        {
            var result = await _taskService.GetTasksAuthor(request.authorId);
            return Ok(result);
        }

        [HttpGet("GetTasksExecutor")]
        public async Task<IActionResult> GetTasksExecutor([FromQuery] TasksByExecutorIdRequest request)
        {
            var result = await _taskService.GetTasksExecutor(request.executorId);
            return Ok(result);
        }

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById([FromQuery] TaskByIdRequest request)
        {
            var result = await _taskService.GetTaskById(request.taskId);
            return Ok(result);
        }

        [HttpPut("DeleteTaskExecutor")]
        public async Task<IActionResult> DeleteTaskExecutor(DeleteTaskExecutorRequest request)
        {
            var result = await _taskService.DeleteTaskExecutor(request.taskId);
            return Ok(result);
        }

        [HttpPut("AddTaskExecutor")]
        public async Task<IActionResult> AddTaskExecutor(AddTaskExecutorRequest request)
        {
            var result = await _taskService.AddTaskExecutor(request.taskId, request.executorId);
            return Ok(result);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(NewTaskRequest task)
        {
            _rabbitMqService.SendCreateTaskMessage(task.AuthorId, task.Price);
            var result = await _taskService.CreateTask(task);
            return Ok(result);
        }

        [HttpGet("GetFilteredTasks")]
        public async Task<IActionResult> GetFilteredTask([FromQuery] FilterTasksRequest filter)
        {
            var result = await _taskService.GetFilteredTasks(filter);
            return Ok(result);
        }

        [HttpPut("CompleteTask")]
        public async Task<IActionResult> CompleteTask(CompleteTaskRequest request)
        {
            var result = await _taskService.CompleteTask(request.taskId);
            if(result != 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
