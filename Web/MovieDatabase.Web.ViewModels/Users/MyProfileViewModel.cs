namespace MovieDatabase.Web.ViewModels
{
    using System.Collections.Generic;

    using MovieDatabase.Web.ViewModels.Users;

    public class MyProfileViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<MyProfileMoviesViewModel> MyMovies { get; set; }
    }
}
