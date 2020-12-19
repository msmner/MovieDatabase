namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public UsersService(IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.moviesRepository = moviesRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentsRepository = commentsRepository;
        }

        public IEnumerable<T> GetMovies<T>(string userId, int page, int itemsPerPage)
        {
            var movies = this.moviesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>().ToList();
            return movies;
        }

        public IEnumerable<T> GetReviews<T>(string userId)
        {
            return this.reviewsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetComments<T>(string userId)
        {
            return this.commentsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();
        }

        public string GetUserByMovieId(int? movieId)
        {
            var movie = this.moviesRepository.All().Where(x => x.Id == movieId).FirstOrDefault();
            return movie.UserId;
        }
    }
}
