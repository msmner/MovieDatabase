namespace MovieDatabase.Web.ViewModels.Movies
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class HomePageMovieViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string FirstQuote { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, HomePageMovieViewModel>()
                .ForMember(x => x.FirstQuote, opt => opt.MapFrom(y => y.Review.FirstQuote));
        }
    }
}
