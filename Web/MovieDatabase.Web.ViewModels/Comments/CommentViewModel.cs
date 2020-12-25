namespace MovieDatabase.Web.ViewModels.Comments
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
               .ForMember(x => x.VotesCount, options =>
               {
                   options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
               });
        }
    }
}
