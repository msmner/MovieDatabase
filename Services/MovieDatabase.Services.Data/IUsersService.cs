namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetMyMovies<T>(string userId, int? take = null, int skip = 0);

        string GetUserByMovieId(int? movieId);
    }
}
