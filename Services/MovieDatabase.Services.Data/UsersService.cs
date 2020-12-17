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

        public UsersService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public IEnumerable<T> GetMyMovies<T>(string userId, int page, int itemsPerPage)
        {
            var movies = this.moviesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>().ToList();
            return movies;
        }

        public string GetUserByMovieId(int? movieId)
        {
            var movie = this.moviesRepository.All().Where(x => x.Id == movieId).FirstOrDefault();
            return movie.UserId;
        }
    }
}
