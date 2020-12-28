namespace MovieDatabase.Web.ViewModels.Comments
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class SingleCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string MovieTitle { get; set; }

        public int ReviewId { get; set; }

        public string SanitizedContent => WebUtility.HtmlDecode(Regex.Replace(new HtmlSanitizer().Sanitize(this.Content), @"<[^>]+>", string.Empty));

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, SingleCommentViewModel>()
                .ForMember(x => x.MovieTitle, opt => opt.MapFrom(y => y.Review.Movie.Title));
        }
    }
}
