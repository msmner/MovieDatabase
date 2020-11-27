namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Movies;

    public interface IMoviesService
    {
        Task<int> AddMovieAsync(string title, string imageUrl, string userId, List<string> genres);

        IEnumerable<T> GetTop10MoviesWithHighestRating<T>(int count = 10);

        Task Delete(string userId, int movieId);

        T GetById<T>(int movieId);

        int GetMoviesCountByUserId(string userId);

        IEnumerable<T> GetMoviesByGenre<T>(string genre);
    }
}
