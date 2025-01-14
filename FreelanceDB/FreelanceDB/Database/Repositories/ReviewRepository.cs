﻿using FreelanceDB.Contracts.Requests;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Entities;
using FreelanceDB.Database.Repositories.Repository;
using FreelanceDB.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FreelancedbContext _context;
        private readonly ILogger<ReviewRepository> _logger;

        public ReviewRepository(FreelancedbContext context, ILogger<ReviewRepository> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogWarning($"Attempt to delete a non-existent review {DateTime.Now.ToString()}");
                throw new Exception("Unknown review.id");
            }

            return id;
        }

        public async Task<List<ReviewModel>> GetAllReviewsByAuthor(long AuthorId)
        {
            var reviews = await _context.Reviews.Where(p => p.AuthorId == AuthorId).ToListAsync();

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

        public async Task<List<ReviewModel>> GetAllReviewsByRecipient(long RecipientId)
        {
            var reviews = await _context.Reviews.Where(p => p.RecipientId == RecipientId).ToListAsync();

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
                _logger.LogWarning($"Attempt to get a non-existent resume {DateTime.Now.ToString()}");
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
