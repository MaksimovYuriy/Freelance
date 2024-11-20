using FreelanceDB.Abstractions;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FreelanceDbContext _context;

        public ReviewRepository(FreelanceDbContext context)
        {
            _context = context;
        }

        public async Task<long> CreateReview(ReviewModel model)
        {
            Review review = new Review()
            {
                Description = model.Description,
                Rate = model.Rate,
                AuthorId = model.AuthorId,
                RecipientId = model.RecipientId
            };

            await _context.AddAsync(review);
            await _context.SaveChangesAsync();

            return review.Id;
        }

        public async Task<long> DeleteReview(long id)
        {
            var status = await _context.Reviews.Where(review => review.Id == id).ExecuteDeleteAsync();

            if(status == 0)
            {
                throw new Exception("Unknown review.id");
            }

            return id;
        }

        public async Task<List<ReviewModel>> GetAllReviews()
        {
            var reviews = await _context.Reviews.AsNoTracking().ToListAsync();

            var reviews_list = new List<ReviewModel>();

            foreach (var review in reviews)
            {
                ReviewModel model = new ReviewModel()
                {
                    Id = review.Id,
                    Description = review.Description,
                    Rate = review.Rate,
                    AuthorId = review.AuthorId,
                    RecipientId = review.RecipientId
                };

                reviews_list.Add(model);
            }

            return reviews_list;
        }

        public async Task<ReviewModel> GetReview(long id)
        {
            var review = await _context.Reviews.Where(review => review.Id == id).FirstOrDefaultAsync();

            if(review == null)
            {
                throw new Exception("Unknown review.id");
            }

            ReviewModel reviewModel = new ReviewModel()
            {
                Id = review.Id,
                Description = review.Description,
                Rate = review.Rate,
                AuthorId = review.AuthorId,
                RecipientId = review.RecipientId
            };

            return reviewModel;
        }
    }
}
