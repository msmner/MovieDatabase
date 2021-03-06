﻿namespace MovieDatabase.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.reviewsRepository = reviewsRepository;
            this.moviesRepository = moviesRepository;
        }

        public async Task AddReviewAsync(int movieId, string content, int rating, string userId)
        {
            var review = new Review
            {
                MovieId = movieId,
                Content = content,
                Rating = rating,
                UserId = userId,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public async Task<T> GetReviewByMovieIdAsync<T>(int movieId)
        {
            var viewModel = await this.reviewsRepository.All().Where(x => x.MovieId == movieId).To<T>().FirstOrDefaultAsync();
            return viewModel;
        }
    }
}
