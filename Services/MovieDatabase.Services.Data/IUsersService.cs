namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;

    public interface IUsersService
    {
        Task<IEnumerable<T>> GetMoviesByUserAsync<T>(string userId, int page, int itemsPerPage);

        Task<IEnumerable<T>> GetReviewsByUserAsync<T>(string userId);

        Task<IEnumerable<T>> GetCommentsByUserAsync<T>(string userId);

        Task<ApplicationUser> GetUserByMovieIdAsync(int? movieId);

        int CommentsCountByUserId(string userId);

        int VotesCountByUserId(string userId);

        Task<IEnumerable<T>> GetMostCommentedReviewsByUserId<T>(string userId);

        Task<IEnumerable<T>> GetMostVotedReviewsByUserId<T>(string userId);
    }
}
