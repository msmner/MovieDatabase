namespace MovieDatabase.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Web.ViewModels.Users;

    public class UsersMoviesViewModel
    {
        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; }

        public int MoviesCount { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.MoviesCount / this.ItemsPerPage);

        public IEnumerable<UsersMovieViewModel> MyMovies { get; set; }
    }
}
