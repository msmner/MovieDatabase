namespace MovieDatabase.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class MoviesByGenreViewModel
    {
        public IEnumerable<MovieDetailsViewModel> MoviesByGenre { get; set; }
    }
}
