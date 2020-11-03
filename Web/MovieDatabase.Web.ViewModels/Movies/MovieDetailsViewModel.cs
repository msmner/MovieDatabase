namespace MovieDatabase.Web.ViewModels.Movies
{
    using AutoMapper;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string ReviewContent { get; set; }

        public string MovieQuotes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(x => x.ReviewContent, opt => opt.MapFrom(x => x.Review.Content));
        }
    }
}
