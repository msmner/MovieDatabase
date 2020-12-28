namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Movies;

    public interface IMoviesService
    {
        Task<int> CreateMovieAsync(string title, string imageUrl, string userId, int[] genres, string quote, string description);

        Task<IEnumerable<T>> GetMoviesWithMostComments<T>(int count = 9);

        Task Delete(int movieId);

        Task<T> GetByIdAsync<T>(int movieId);

        int GetMoviesCountByUserId(string userId);

        Task<IEnumerable<T>> GetMoviesByGenreAsync<T>(string genre);

        Task<IEnumerable<T>> GetMoviesByTitleAsync<T>(string searchString);

        int GetMoviesCountByGenre(string genre);

        Task UpdateAsync(EditMovieViewModel input);
    }
}
