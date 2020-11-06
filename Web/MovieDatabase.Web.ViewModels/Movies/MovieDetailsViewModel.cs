namespace MovieDatabase.Web.ViewModels.Movies
{
    using AutoMapper;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string FirstQuote { get; set; }

        public string SecondQuote { get; set; }

        public string ThirdQuote { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(x => x.FirstQuote, opt => opt.MapFrom(y => y.Review.FirstQuote))
                .ForMember(x => x.SecondQuote, opt => opt.MapFrom(y => y.Review.SecondQuote))
                .ForMember(x => x.ThirdQuote, opt => opt.MapFrom(y => y.Review.ThirdQuote))
                .ForMember(x => x.Content, opt => opt.MapFrom(y => y.Review.Content));
        }
    }
}
