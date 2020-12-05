namespace MovieDatabase.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MoviesViewModel
    {
        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; }

        public int MoviesCount { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.MoviesCount / this.ItemsPerPage);

        public string SearchString { get; set; }

        public IEnumerable<MovieDetailsViewModel> MyMovies { get; set; }

        public IEnumerable<MovieDetailsViewModel> Top10MoviesWithHighestVoteCount { get; set; }
    }
}
