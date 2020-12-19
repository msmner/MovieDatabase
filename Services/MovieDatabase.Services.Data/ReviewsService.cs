namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.reviewsRepository = reviewsRepository;
            this.moviesRepository = moviesRepository;
        }

        public async Task AddReviewAsync(int movieId, string content, int rating)
        {
            var review = new Review
            {
                MovieId = movieId,
                Content = content,
                Rating = rating,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();

            var movie = this.moviesRepository.All().Where(x => x.Id == movieId).FirstOrDefault();
            movie.Review = review;
            await this.moviesRepository.SaveChangesAsync();
        }

        public T GetReviewByMovieId<T>(int movieId)
        {
            var viewModel = this.reviewsRepository.All().Where(x => x.MovieId == movieId).To<T>().FirstOrDefault();
            return viewModel;
        }
    }
}
