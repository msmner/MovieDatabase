namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels.Movies;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IRepository<MovieGenre> moviesGenresRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository, IRepository<Genre> genresRepository, IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Comment> commentsRepository, IRepository<MovieGenre> moviesGenresRepository)
        {
            this.moviesRepository = moviesRepository;
            this.genresRepository = genresRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentsRepository = commentsRepository;
            this.moviesGenresRepository = moviesGenresRepository;
        }

        public async Task<int> CreateMovieAsync(string title, string imageUrl, string userId, int[] genres, string quote, string description)
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
                var movieGenre = new MovieGenre { Movie = movie, Genre = genre };
                movie.MovieGenres.Add(movieGenre);
            }

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();

            return movie.Id;
        }

        public async Task Delete(int movieId)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);
            this.moviesRepository.Delete(movie);

            var review = this.reviewsRepository.All().FirstOrDefault(x => x.MovieId == movieId);

            if (review != null)
            {
                this.reviewsRepository.Delete(review);
            }

            var comments = this.commentsRepository.All().Where(x => x.ReviewId == movieId);

            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    this.commentsRepository.Delete(comment);
                }
            }

            await this.commentsRepository.SaveChangesAsync();
            await this.reviewsRepository.SaveChangesAsync();
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetMoviesWithMostComments<T>(int count = 9)
        {
            return await this.moviesRepository.All().OrderByDescending(x => x.Review.Comments.Count()).Take(count).To<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int movieId)
        {
            var movie = await this.moviesRepository.All().Where(x => x.Id == movieId).To<T>().FirstOrDefaultAsync();
            return movie;
        }

        public int GetMoviesCountByUserId(string userId)
        {
            return this.moviesRepository.All().Where(x => x.UserId == userId).Count();
        }

        public async Task<IEnumerable<T>> GetMoviesByGenreAsync<T>(string genre)
        {
            return await this.moviesRepository
                .AllAsNoTracking()
                .Where(x => x.MovieGenres.Any(y => y.Genre.Type == genre))
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
        }

        public int GetMoviesCountByGenre(string genre)
        {
            return this.moviesRepository.All().Where(x => x.MovieGenres.Any(x => x.Genre.Type == genre)).Count();
        }

        public async Task<IEnumerable<T>> GetMoviesByTitleAsync<T>(string searchString)
        {
            return await this.moviesRepository.All()
                .Where(x => x.Title.Contains(searchString))
                .To<T>()
                .ToListAsync();
        }

        public async Task UpdateAsync(EditMovieViewModel input)
        {
            var movie = await this.moviesRepository.All().FirstOrDefaultAsync(x => x.Id == input.Id);

            var movieGenres = await this.moviesGenresRepository.All().Where(x => x.MovieId == input.Id).ToListAsync();

            foreach (var movieGenre in movieGenres)
            {
                this.moviesGenresRepository.Delete(movieGenre);
            }

            await this.moviesGenresRepository.SaveChangesAsync();

            foreach (var genreId in input.GenreIds)
            {
                var genre = await this.genresRepository.All().FirstOrDefaultAsync(x => x.Id == genreId);
                movie.MovieGenres.Add(new MovieGenre { Movie = movie, Genre = genre });
            }

            movie.Title = input.Title;
            movie.Description = input.Description;
            movie.Quote = input.Quote;
            movie.ImageUrl = input.NewImageUrl;

            await this.moviesRepository.SaveChangesAsync();
        }
    }
}
