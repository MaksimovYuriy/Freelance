using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests.TaskRequests;
using FreelanceDB.Contracts.Responses.TaskResponses;
using FreelanceDB.Models;
using FreelanceDB.RabbitMQ;
using FreelanceDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text.Json;

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
            TasksResponse response = new TasksResponse(tasks: result);
            return Ok(response);
        }

        [HttpGet("GetTasksAuthor")]
        public async Task<IActionResult> GetTasksAuthor([FromQuery] TasksByAuthorIdRequest request)
        {
            var result = await _taskService.GetTasksAuthor(request.authorId);
            TasksResponse response = new TasksResponse(tasks: result);
            return Ok(response);
        }

        [HttpGet("GetTasksExecutor")]
        public async Task<IActionResult> GetTasksExecutor([FromQuery] TasksByExecutorIdRequest request)
        {
            var result = await _taskService.GetTasksExecutor(request.executorId);
            TasksResponse response = new TasksResponse(tasks: result);
            return Ok(response);
        }

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById([FromQuery] TaskByIdRequest request)
        {
            var result = await _taskService.GetTaskById(request.taskId);
            TaskByIdResponse response = new TaskByIdResponse(task: result);
            return Ok(response);
        }

        [HttpPut("DeleteTaskExecutor")]
        public async Task<IActionResult> DeleteTaskExecutor(DeleteTaskExecutorRequest request)
        {
            var result = await _taskService.DeleteTaskExecutor(request.taskId);
            DeleteTaskExecutorResponse response = new DeleteTaskExecutorResponse(deletedExecutors: result);
            return Ok(response);
        }

        [HttpPut("AddTaskExecutor")]
        public async Task<IActionResult> AddTaskExecutor(AddTaskExecutorRequest request)
        {
            var result = await _taskService.AddTaskExecutor(request.taskId, request.executorId);
            AddTaskExecutorResponse response = new AddTaskExecutorResponse(addedExecutors: result);
            return Ok(response);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(NewTaskRequest task)
        {
            _rabbitMqService.SendCreateTaskMessage(task.AuthorId, task.Price);
            var result = await _taskService.CreateTask(task);
            CreateTaskResponse response = new CreateTaskResponse(newTaskId: result);
            return Ok(response);
        }

        [HttpGet("GetFilteredTasks")]
        public async Task<IActionResult> GetFilteredTask([FromQuery] FilterTasksRequest filter)
        {
            var result = await _taskService.GetFilteredTasks(filter);
            TasksResponse response = new TasksResponse(tasks: result);
            return Ok(response);
        }

        [HttpPut("CompleteTask")]
        public async Task<IActionResult> CompleteTask(CompleteTaskRequest request)
        {
            var result = await _taskService.CompleteTask(request.taskId);
            CompleteTaskResponse response = new CompleteTaskResponse(completedTasks: result);
            if(result != 0)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}
