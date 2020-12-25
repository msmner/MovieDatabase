namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class SingleReviewViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string MovieTitle { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public int VotesCount { get; set; }

        public string SanitizedShortContent => this.Content.Length > 100 ? WebUtility.HtmlDecode(Regex.Replace(new HtmlSanitizer().Sanitize(this.Content.Substring(0, 100)), @"<[^>]+>", string.Empty)) : WebUtility.HtmlDecode(Regex.Replace(new HtmlSanitizer().Sanitize(this.Content), @"<[^>]+>", string.Empty));

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, SingleReviewViewModel>()
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
                });
        }
    }
}
