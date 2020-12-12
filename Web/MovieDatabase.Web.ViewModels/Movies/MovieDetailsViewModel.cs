namespace MovieDatabase.Web.ViewModels.Movies
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int ReviewId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Quote { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        [Required]
        public string UserUserName { get; set; }

        [Required]
        public int CommentsCount { get; set; }

        public int? VotesCount { get; set; }

        [Required]
        public string[] Genres { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(y => y.Review.Comments.Count))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(y => y.Review.Votes.Sum(x => (int)x.Type)))
                .ForMember(x => x.Genres, opt => opt.MapFrom(y => y.Genres.Select(g => g.Type)));
        }
    }
}
