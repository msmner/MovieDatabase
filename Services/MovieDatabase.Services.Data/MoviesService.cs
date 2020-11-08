namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using System.Linq;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public async Task<int> AddMovieAsync(string title, string imageUrl, string userId)
        {
            var movie = new Movie
            {
                UserId = userId,
                Title = title,
                ImageUrl = imageUrl,
            };

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
                await this.moviesRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll<T>(int? count = 10)
        {
            return this.moviesRepository.All().To<T>().ToList();
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
    }
}
