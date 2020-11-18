namespace MovieDatabase.Web.ViewModels.Movies
{
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class CreateMovieInputViewModel : IMapTo<Movie>
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string[] GenreIds { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}
