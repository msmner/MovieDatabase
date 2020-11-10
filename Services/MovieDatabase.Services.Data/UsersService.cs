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

        public IEnumerable<T> GetMyMovies<T>(string userId)
        {
            var movies = this.moviesRepository.All().Where(x => x.UserId == userId).To<T>().ToList();
            return movies;
        }

        public string GetUserByMovieId(int? movieId)
        {
            var movie = this.moviesRepository.All().Where(x => x.Id == movieId).FirstOrDefault();
            return movie.UserId;
        }
    }
}
