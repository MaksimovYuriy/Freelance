using FreelanceDB.Contracts.Requests.ReviewRequests;
using FreelanceDB.Contracts.Responses.ReviewResponses;
using FreelanceDB.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CalculateUserRate([FromQuery] CalculateRateRequest request)
        {
            var result = await _reviewService.CalculateUserRate(request.userId);
            CalculateRateResponse response = new CalculateRateResponse(rate: result);

            if (result >= 0 && result <= 5)
            {
                _logger.LogInformation($"Get user rate, userId: {request.userId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"UserRate icorrect, userId: {request.userId} " + DateTime.Now.ToString());
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReviewsAuthor([FromQuery] ReviewsAuthorRequest request)
        {
            var result = await _reviewService.GetReviewsByAuthor(request.authorId);
            ReviewsResponse response = new ReviewsResponse(reviews: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation($"Get reviews where user - author, userId: {request.authorId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Reviews where user - author not found, userId: {request.authorId} " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReviewsRecipient([FromQuery] ReviewsRecipientRequest request)
        {
            var result = await _reviewService.GetReviewsByRecipient(request.recipientId);
            ReviewsResponse response = new ReviewsResponse(reviews: result);

            if (result != null && result.Any())
            {
                _logger.LogInformation($"Get reviews where user - recipient, userId: {request.recipientId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Reviews where user - recipient not found, userId: {request.recipientId} " + DateTime.Now.ToString());
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> NewReview(NewReviewRequest request)
        {
            var result = await _reviewService.CreateReview(request);
            NewReviewResponse response = new NewReviewResponse(newReviewId: result);
            
            if(result != 0)
            {
                _logger.LogInformation($"Create new review, id: {result} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogInformation("New review not created " + DateTime.Now.ToString());
            return BadRequest(response);
        }
    }
}
