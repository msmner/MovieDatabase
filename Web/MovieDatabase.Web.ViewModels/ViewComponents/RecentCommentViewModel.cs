namespace MovieDatabase.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class RecentCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(150)]
        public string Content { get; set; }

        [Required]
        [MaxLength(80)]
        public string MovieTitle { get; set; }

        [Required]
        [MaxLength(150)]
        public string SanitizedContent => this.Content.Length > 150 ? new HtmlSanitizer().Sanitize(this.Content.Substring(0, 150) + "...") : new HtmlSanitizer().Sanitize(this.Content);

        [Required]
        public string UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, RecentCommentViewModel>()
                .ForMember(x => x.MovieTitle, options => options.MapFrom(y => y.Review.Movie.Title))
                .ForMember(x => x.MovieId, options => options.MapFrom(y => y.Review.Movie.Id));
        }
    }
}
