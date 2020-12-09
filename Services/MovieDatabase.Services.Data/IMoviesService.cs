namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMoviesService
    {
        Task<int> AddMovieAsync(string title, string imageUrl, string userId, List<int> genres);

        IEnumerable<T> GetTop10MoviesWithHighestRating<T>(int count = 10);

        Task Delete(int movieId);

        T GetById<T>(int movieId);

        int GetMoviesCountByUserId(string userId);

        IEnumerable<T> GetMoviesByGenre<T>(string genre);

        IEnumerable<T> GetMoviesByTitle<T>(string searchString);

        Task<bool> IsMovieCreatorLoggedIn(string userId, int movieId);
    }
}
