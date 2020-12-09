namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class ReviewDetailsViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(500)]
        public string Content { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string UserUserName { get; set; }

        [Required]
        public int VotesCount { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MaxLength(80)]
        public string MovieTitle { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(500)]
        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public IEnumerable<ReviewCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, ReviewDetailsViewModel>()
                .ForMember(x => x.MovieTitle, opt => opt.MapFrom(y => y.Movie.Title))
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
                });
        }
    }
}
