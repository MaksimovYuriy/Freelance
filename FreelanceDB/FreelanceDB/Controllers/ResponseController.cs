using FreelanceDB.Contracts.Requests;
using FreelanceDB.Contracts.Requests.RespRequests;
using FreelanceDB.Contracts.Responses.RespResponses;
using FreelanceDB.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelanceDB.Services.Services;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpPost]
        public async Task<IActionResult> CreateTaskResponse([FromQuery] CreateRespRequest request)
        {
            var result = await _responseService.CreateTaskResponse(request.taskId, request.userId);
            CreateRespResponse response = new CreateRespResponse(newResponseId: result);

            if(result != 0)
            {
                _logger.LogInformation($"Create response on task: {request.taskId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Response on task: {request.taskId} not created " + DateTime.Now.ToString());
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyResponses([FromQuery] MyRespsRequest request)
        {
            var result = await _responseService.GetMyResposes(request.userId);
            RespsResponse response = new RespsResponse(responses: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation($"Get responses by userId: {request.userId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Response by userId: {request.userId} not found " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskResponses([FromQuery] TaskRespsRequest request)
        {
            var result = await _responseService.GetTaskResponses(request.taskId);
            RespsResponse response = new RespsResponse(responses: result);

            if(result != null && result.Any())
            {
                _logger.LogInformation($"Get responses by taskId: {request.taskId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Response by taskId: {request.taskId} not found " + DateTime.Now.ToString());
            return NotFound(response);
        }
    }
}
