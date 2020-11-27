namespace MovieDatabase.Web.ViewModels.Movies
{
    using System.Collections;
    using System.Collections.Generic;

    public class HomePageMoviesViewModel
    {
        public IEnumerable<HomePageMovieViewModel> Top10MoviesWithHighestVoteCount { get; set; }
    }
}
