using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests.ReviewRequests;
using FreelanceDB.Contracts.Responses.ReviewResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("CalculateUserRate")]
        public async Task<IActionResult> CalculateUserRate([FromQuery] CalculateRateRequest request)
        {
            var result = await _reviewService.CalculateUserRate(request.userId);
            CalculateRateResponse response = new CalculateRateResponse(rate: result);
            return Ok(response);
        }

        [HttpGet("ReviewsAuthor")]
        public async Task<IActionResult> ReviewsAuthor([FromQuery] ReviewsAuthorRequest request)
        {
            var result = await _reviewService.GetReviewsByAuthor(request.authorId);
            ReviewsResponse response = new ReviewsResponse(reviews: result);
            return Ok(response);
        }

        [HttpGet("ReviewsRecipient")]
        public async Task<IActionResult> ReviewsRecipient([FromQuery] ReviewsRecipientRequest request)
        {
            var result = await _reviewService.GetReviewsByRecipient(request.recipientId);
            ReviewsResponse response = new ReviewsResponse(reviews: result);
            return Ok(response);
        }

        [HttpPost("NewReview")]
        public async Task<IActionResult> NewReview(NewReviewRequest request)
        {
            var result = await _reviewService.CreateReview(request);
            NewReviewResponse response = new NewReviewResponse(newReviewId: result);
            return Ok(response);
        }
    }
}
