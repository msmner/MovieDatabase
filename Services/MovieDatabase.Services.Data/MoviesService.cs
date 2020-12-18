namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository, IRepository<Genre> genresRepository, IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.moviesRepository = moviesRepository;
            this.genresRepository = genresRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentsRepository = commentsRepository;
        }

        public async Task<int> AddMovieAsync(string title, string imageUrl, string userId, List<int> genres, string quote, string description)
        {
            var movie = new Movie
            {
                UserId = userId,
                Title = title,
                ImageUrl = imageUrl,
                Quote = quote,
                Description = description,
            };

            foreach (var genreId in genres)
            {
                var genre = this.genresRepository.All().FirstOrDefault(x => x.Id == genreId);
                var genreToAdd = new Genre { Type = genre.Type };
                movie.Genres.Add(genreToAdd);
            }

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();

            return movie.Id;
        }

        public async Task<bool> IsMovieCreatorLoggedIn(string userId, int movieId)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            if (movie.IsDeleted == false && movie.UserId == userId)
            {
                return true;
            }

            return false;
        }

        public async Task Delete(int movieId)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);
            this.moviesRepository.Delete(movie);

            var review = this.reviewsRepository.All().FirstOrDefault(x => x.MovieId == movieId);
            this.reviewsRepository.Delete(review);

            var comments = this.commentsRepository.All().Where(x => x.ReviewId == movieId);

            foreach (var comment in comments)
            {
                this.commentsRepository.Delete(comment);
            }

            await this.commentsRepository.SaveChangesAsync();
            await this.reviewsRepository.SaveChangesAsync();
            await this.moviesRepository.SaveChangesAsync();
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

        public int GetMoviesCountByGenre(string genre)
        {
            return this.moviesRepository.All().Where(x => x.Genres.Any(x => x.Type == genre)).Count();
        }

        public IEnumerable<T> GetMoviesByTitle<T>(string searchString)
        {
            return this.moviesRepository.All()
                .Where(x => x.Title.Contains(searchString))
                .To<T>()
                .ToList();
        }
    }
}
