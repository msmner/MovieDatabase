﻿namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetMyMovies<T>(string userId, int page, int itemsPerPage);

        string GetUserByMovieId(int? movieId);
    }
}
