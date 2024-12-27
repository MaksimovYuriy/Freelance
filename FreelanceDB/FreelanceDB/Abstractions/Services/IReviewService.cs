using FreelanceDB.Contracts.Requests.ReviewRequests;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Services
{
    public interface IReviewService
    {
        Task<long> CreateReview(NewReviewRequest request);
        Task<long> CalculateUserRate(long userId);
        Task<List<ReviewModel>> GetReviewsByAuthor(long authorId);
        Task<List<ReviewModel>> GetReviewsByRecipient(long recipientId);
    }
}
