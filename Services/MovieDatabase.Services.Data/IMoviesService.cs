namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Web.ViewModels.Movies;

    public interface IMoviesService
    {
        Task<int> AddMovieAsync(string description, string title, string imageUrl, string userId);

        IEnumerable<T> GetAll<T>(int? count = 10);

        Task Delete(string userId, int movieId);

        public T GetById<T>(int movieId);
    }
}
