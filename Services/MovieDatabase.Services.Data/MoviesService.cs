namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<Genre> genresRepository)
        {
            this.moviesRepository = moviesRepository;
            this.genresRepository = genresRepository;
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
