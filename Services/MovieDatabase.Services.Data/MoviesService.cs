namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ForumSystem.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IRepository<Vote> votesRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<Genre> genresRepository, IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.moviesRepository = moviesRepository;
            this.genresRepository = genresRepository;
            this.reviewsRepository = reviewsRepository;
            this.votesRepository = votesRepository;
        }

        public async Task<int> AddMovieAsync(string title, string imageUrl, string userId, List<string> genres)
        {
            var movie = new Movie
            {
                UserId = userId,
                Title = title,
                ImageUrl = imageUrl,
            };

            foreach (var genreId in genres)
            {
                movie.Genres.Add(this.genresRepository.All().FirstOrDefault(x => x.Id == genreId));
            }


            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();

            return movie.Id;
        }

        public async Task Delete(string userId, int movieId)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);
            if (movie.IsDeleted == false && movie.UserId == userId)
            {
                this.moviesRepository.Delete(movie);
                var review = this.reviewsRepository.All().FirstOrDefault(x => x.MovieId == movieId);
                this.reviewsRepository.Delete(review);

                await this.reviewsRepository.SaveChangesAsync();
                await this.moviesRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetTop10MoviesWithHighestRating<T>(int count = 10)
        {
            return this.moviesRepository.All().OrderByDescending(x => x.Votes.Sum(y => (int)y.Type)).Take(count).To<T>().ToList();
        }

        public T GetById<T>(int movieId)
        {
            var movie = this.moviesRepository.All().Where(x => x.Id == movieId).To<T>().FirstOrDefault();
            return movie;
        }

        public int GetMoviesCountByUserId(string userId)
        {
            return this.moviesRepository.All().Where(x => x.UserId == userId).Count();
        }

        public IEnumerable<T> GetMoviesByGenre<T>(string genre)
        {
            return this.moviesRepository
                .AllAsNoTracking()
                .Where(x => x.Genres.Any(y => y.Type == genre))
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToList();
        }
    }
}
