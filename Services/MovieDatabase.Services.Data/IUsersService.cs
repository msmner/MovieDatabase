namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetMyMovies<T>(string userId);

        string GetUserByMovieId(int? movieId);
    }
}
