namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public IEnumerable<T> GetMyMovies<T>(string userId, int? take = null, int skip = 0)
        {
            var movies = this.moviesRepository.All().OrderByDescending(x => x.CreatedOn).Where(x => x.UserId == userId).Skip(skip);
            if (take.HasValue)
            {
                movies = movies.Take(take.Value);
            }

            return movies.To<T>().ToList();
        }

        public string GetUserByMovieId(int? movieId)
        {
            var movie = this.moviesRepository.All().Where(x => x.Id == movieId).FirstOrDefault();
            return movie.UserId;
        }
    }
}
