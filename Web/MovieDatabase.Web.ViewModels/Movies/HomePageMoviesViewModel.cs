using System.Collections;
using System.Collections.Generic;

namespace MovieDatabase.Web.ViewModels.Movies
{
    public class HomePageMoviesViewModel
    {
        public IEnumerable<HomePageMovieViewModel> Movies { get; set; }
    }
}
