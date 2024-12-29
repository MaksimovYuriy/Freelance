using FreelanceDB.Contracts.Requests.TaskRequests;
using FreelanceDB.Contracts.Responses.TaskResponses;
using FreelanceDB.RabbitMQ;
using FreelanceDB.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasks();
            TasksResponse response = new TasksResponse(tasks: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation("Get all tasks " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning("Tasks not found " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksAuthor([FromQuery] TasksByAuthorIdRequest request)
        {
            var result = await _taskService.GetTasksAuthor(request.authorId);
            TasksResponse response = new TasksResponse(tasks: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation($"Get all tasks by AuthorId: {request.authorId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Tasks not found by AuthorId: {request.authorId} " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksExecutor([FromQuery] TasksByExecutorIdRequest request)
        {
            var result = await _taskService.GetTasksExecutor(request.executorId);
            TasksResponse response = new TasksResponse(tasks: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation($"Get all tasks by ExecutorId: {request.executorId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Tasks by ExecutorId: {request.executorId} not found " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskById([FromQuery] TaskByIdRequest request)
        {
            var result = await _taskService.GetTaskById(request.taskId);
            TaskByIdResponse response = new TaskByIdResponse(task: result);

            if (result != null)
            {
                _logger.LogInformation($"Get task by Id: {request.taskId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Tasks by Id: {request.taskId} not found " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> DeleteTaskExecutor(DeleteTaskExecutorRequest request)
        {
            var result = await _taskService.DeleteTaskExecutor(request.taskId);
            DeleteTaskExecutorResponse response = new DeleteTaskExecutorResponse(deletedExecutors: result);

            if (result != 0)
            {
                _logger.LogInformation($"Delete task-executor, taskId: {request.taskId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Task by Id: {request.taskId} not found " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> AddTaskExecutor(AddTaskExecutorRequest request)
        {
            var result = await _taskService.AddTaskExecutor(request.taskId, request.executorId);
            AddTaskExecutorResponse response = new AddTaskExecutorResponse(addedExecutors: result);

            if (result != 0)
            {
                _logger.LogInformation($"Add task-executor, taskId: {request.taskId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Task-executor: {request.executorId} not added, taskId: {request.taskId} " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(NewTaskRequest task)
        {
            _rabbitMqService.SendCreateTaskMessage(task.AuthorId, task.Price);
            var result = await _taskService.CreateTask(task);
            CreateTaskResponse response = new CreateTaskResponse(newTaskId: result);

            if (result != 0)
            {
                _logger.LogInformation($"Create task, id: {result} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Task not created, id: {result} " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredTask([FromQuery] FilterTasksRequest filter)
        {
            var result = await _taskService.GetFilteredTasks(filter);
            TasksResponse response = new TasksResponse(tasks: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation("Get all tasks with filter " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning("Tasks with filter not found " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> CompleteTask(CompleteTaskRequest request)
        {
            var result = await _taskService.CompleteTask(request.taskId);
            CompleteTaskResponse response = new CompleteTaskResponse(completedTasks: result);
            if(result != 0)
            {
                _logger.LogInformation($"Complete task, id:{result} " + DateTime.Now.ToString());
                return Ok(response);
            }
            else
            {
                _logger.LogInformation($"Task not completed, id:{result} " + DateTime.Now.ToString());
                return NotFound(response);
            }
        }
    }
}
