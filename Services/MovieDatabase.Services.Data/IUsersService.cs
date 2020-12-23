namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<IEnumerable<T>> GetMoviesAsync<T>(string userId, int page, int itemsPerPage);

        Task<IEnumerable<T>> GetReviewsAsync<T>(string userId);

        Task<IEnumerable<T>> GetCommentsAsync<T>(string userId);

        Task<string> GetUserByMovieIdAsync(int? movieId);
    }
}
