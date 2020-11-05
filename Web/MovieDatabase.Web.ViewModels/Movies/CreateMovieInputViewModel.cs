namespace MovieDatabase.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class CreateMovieInputViewModel : IMapTo<Movie>
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
