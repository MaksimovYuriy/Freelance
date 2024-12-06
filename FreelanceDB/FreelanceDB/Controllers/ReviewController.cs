using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests;
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
        public async Task<IActionResult> CalculateUserRate(long userId)
        {
            var result = await _reviewService.CalculateUserRate(userId);
            return Ok(result);
        }

        [HttpGet("ReviewsAuthor")]
        public async Task<IActionResult> ReviewsAuthor(long authorId)
        {
            var result = await _reviewService.GetReviewsByAuthor(authorId);
            return Ok(result);
        }

        [HttpGet("ReviewsRecipient")]
        public async Task<IActionResult> ReviewsRecipient(long recipientId)
        {
            var result = await _reviewService.GetReviewsByRecipient(recipientId);
            return Ok(result);
        }

        [HttpPost("NewReview")]
        public async Task<IActionResult> NewReview(NewReviewRequest request)
        {
            var result = await _reviewService.CreateReview(request);
            return Ok(result);
        }
    }
}
