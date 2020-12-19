namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetMovies<T>(string userId, int page, int itemsPerPage);

        IEnumerable<T> GetReviews<T>(string userId);

        IEnumerable<T> GetComments<T>(string userId);

        string GetUserByMovieId(int? movieId);
    }
}
