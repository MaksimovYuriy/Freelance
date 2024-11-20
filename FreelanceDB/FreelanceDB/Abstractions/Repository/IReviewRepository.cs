using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Repository
{
    public interface IReviewRepository
    {
        Task<List<ReviewModel>> GetAllReviews();
        Task<ReviewModel> GetReview(long id);
        Task<long> CreateReview(ReviewModel model);
        Task<long> DeleteReview(long id);
    }
}
