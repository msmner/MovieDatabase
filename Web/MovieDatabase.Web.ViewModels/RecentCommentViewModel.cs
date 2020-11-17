namespace MovieDatabase.Web.ViewModels
{
    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class RecentCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string MovieTitle { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserId { get; set; }

        public int MovieId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, RecentCommentViewModel>()
                .ForMember(x => x.MovieTitle, options => options.MapFrom(y => y.Review.Movie.Title))
                .ForMember(x => x.MovieId, options => options.MapFrom(y => y.Review.Movie.Id));
        }
    }
}
