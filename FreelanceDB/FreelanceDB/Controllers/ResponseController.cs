using FreelanceDB.Contracts.Requests;
using FreelanceDB.Contracts.Requests.RespRequests;
using FreelanceDB.Contracts.Responses.RespResponses;
using FreelanceDB.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelanceDB.Services.Services;

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
        public async Task<IActionResult> CreateTaskResponse([FromQuery] CreateRespRequest request)
        {
            var result = await _responseService.CreateTaskResponse(request.taskId, request.userId);
            CreateRespResponse response = new CreateRespResponse(newResponseId: result);
            return Ok(response);
        }

        [HttpGet("GetMyResponses")]
        public async Task<IActionResult> GetMyResponses([FromQuery] MyRespsRequest request)
        {
            var result = await _responseService.GetMyResposes(request.userId);
            RespsResponse response = new RespsResponse(responses: result);
            return Ok(response);
        }

        [HttpGet("GetTaskResponses")]
        public async Task<IActionResult> GetTaskResponses([FromQuery] TaskRespsRequest request)
        {
            var result = await _responseService.GetTaskResponses(request.taskId);
            RespsResponse response = new RespsResponse(responses: result);
            return Ok(response);
        }
    }
}
