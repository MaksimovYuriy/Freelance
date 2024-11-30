﻿using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Models;
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
        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            _logger.LogInformation("Get tasks " + DateTime.Now.ToString());
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

        [HttpPost("CreateTaskResponse")]
        public async Task<IActionResult> CreateTaskResponse(long taskId, long userId)
        {
            var result = await _taskService.CreateTaskResponse(taskId, userId);
            return Ok(result);
        }

        [HttpGet("GetMyResponses")]
        public async Task<IActionResult> GetMyResponses(long userId)
        {
            var result = await _taskService.GetMyResposes(userId);
            return Ok(result);
        }

        [HttpGet("GetTaskResponses")]
        public async Task<IActionResult> GetTaskResponses(long taskId)
        {
            var result = await _taskService.GetTaskResponses(taskId);
            return Ok(result);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(NewTaskRequest task)
        {
            var result = await _taskService.CreateTask(task);
            return Ok(result);
        }

        [HttpGet("GetFilteredTasks")]
        public async Task<IActionResult> GetFilteredTask([FromQuery] FilterTasksRequest filter)
        {
            var result = await _taskService.GetFilteredTasks(filter);
            return Ok(result);
        }
    }
}
