namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels.Comments;

    public class ReviewDetailsViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string MovieTitle { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public IEnumerable<CommentViewModel> Comments { get; set; }

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
