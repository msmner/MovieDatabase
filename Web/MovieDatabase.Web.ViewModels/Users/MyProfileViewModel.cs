namespace MovieDatabase.Web.ViewModels
{
    using System.Collections.Generic;

    using MovieDatabase.Web.ViewModels.Users;

    public class MyProfileViewModel
    {
        public IEnumerable<MyProfileMoviesViewModel> MyMovies { get; set; }
    }
}
