using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieDatabase.Services.Data
{
    public interface IUsersService
    {
        IEnumerable<T> GetMyMovies<T>(string userId);

        Task<string> GetUserByMovieId(int movieId);
    }
}
