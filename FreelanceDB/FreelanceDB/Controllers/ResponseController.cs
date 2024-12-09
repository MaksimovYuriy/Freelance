using FreelanceDB.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;
        private readonly ILogger<ResponseController> _logger;

        public ResponseController(IResponseService responseService, ILogger<ResponseController> logger)
        {
            _responseService = responseService;
            _logger = logger;
        }

        [HttpPost("CreateTaskResponse")]
        public async Task<IActionResult> CreateTaskResponse(long taskId, long userId)
        {
            var result = await _responseService.CreateTaskResponse(taskId, userId);
            return Ok(result);
        }

        [HttpGet("GetMyResponses")]
        public async Task<IActionResult> GetMyResponses(long userId)
        {
            var result = await _responseService.GetMyResposes(userId);
            return Ok(result);
        }

        [HttpGet("GetTaskResponses")]
        public async Task<IActionResult> GetTaskResponses(long taskId)
        {
            var result = await _responseService.GetTaskResponses(taskId);
            return Ok(result);
        }
    }
}
