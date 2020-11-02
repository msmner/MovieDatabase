namespace MovieDatabase.Web.ViewModels.Movies
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class HomePageMovieViewModel : IMapFrom<Movie>
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string MovieQuotes { get; set; }

        public int TotalRating { get; set; }
    }
}
