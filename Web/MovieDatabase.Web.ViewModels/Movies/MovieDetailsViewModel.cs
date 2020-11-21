namespace MovieDatabase.Web.ViewModels.Movies
{
    using Microsoft.AspNetCore.Http;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MovieDetailsViewModel : IMapFrom<Movie>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int ReviewId { get; set; }
    }
}
