using FreelanceDB.Contracts.Requests;
using FreelanceDB.Models;

namespace FreelanceDB.Abstractions.Repository
{
    public interface IReviewRepository
    {
        Task<List<ReviewModel>> GetAllReviewsByRecipient(long RecipientId);
        Task<List<ReviewModel>> GetAllReviewsByAuthor(long AuthorId);
        Task<ReviewModel> GetReview(long id);
        Task<long> CreateReview(ReviewModel model);
        Task<long> DeleteReview(long id);
    }
}
