using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests.ReviewRequests;
using FreelanceDB.Models;

namespace FreelanceDB.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<long> CalculateUserRate(long userId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByRecipient(userId);

            if(reviews.Count == 0)
            {
                return 0;
            }

            int summary = 0;
            foreach (var review in reviews)
            {
                summary += review.Rate;
            }

            return summary / reviews.Count;
        }

        public async Task<long> CreateReview(NewReviewRequest request)
        {
            ReviewModel model = new ReviewModel()
            {
                Description = request.Description,
                Rate = request.Rate,
                AuthorId = request.AuthorId,
                RecipientId = request.RecipientId
            };

            var reviewId = await _reviewRepository.CreateReview(model);
            return reviewId;
        }

        public async Task<List<ReviewModel>> GetReviewsByAuthor(long authorId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByAuthor(authorId);
            return reviews;
        }

        public async Task<List<ReviewModel>> GetReviewsByRecipient(long recipientId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByRecipient(recipientId);
            return reviews;
        }
    }
}
