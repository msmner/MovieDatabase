namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Movies;
    using MovieDatabase.Services.Mapping;
    using System.Linq;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<UserMovieRating> ratingsRepository;
        private readonly IDeletableEntityRepository<Review> reviewRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<UserMovieRating> ratingsRepository, IDeletableEntityRepository<Review> reviewRepository)
        {
            this.moviesRepository = moviesRepository;
            this.ratingsRepository = ratingsRepository;
            this.reviewRepository = reviewRepository;
        }

        public async Task<int> AddMovieAsync(CreateMovieInputViewModel input, string userId)
        {
            var review = new Review
            {
                Content = input.Review,
            };

            var rating = new UserMovieRating
            {
                UserId = userId,
                Rating = int.Parse(input.Rating),
            };

            var movie = new Movie
            {
                UserId = userId,
                Title = input.Title,
                ImageUrl = input.ImageUrl,
                Rating = int.Parse(input.Rating),
                Review = review,
                MovieQuotes = input.MovieQuotes,
                Description = input.Description,
            };

            await this.ratingsRepository.AddAsync(rating);
            await this.ratingsRepository.SaveChangesAsync();

            await this.reviewRepository.AddAsync(review);
            await this.reviewRepository.SaveChangesAsync();

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();

            return movie.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = 10)
        {
            return this.moviesRepository.All().To<T>().ToList();
        }
    }
}
