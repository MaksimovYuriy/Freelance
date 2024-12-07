using FreelanceDB.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<ResponseController> _logger;

        public ResponseController(ITaskService taskService, ILogger<ResponseController> logger)
        {
            _taskService = taskService;
            _logger = logger;
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
    }
}
