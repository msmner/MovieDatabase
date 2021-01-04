namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using ForumSystem.Data.Models;
    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class ReviewStatisticsViewModel : IMapFrom<Review>
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => Regex.Replace(new HtmlSanitizer().Sanitize(this.Content), @"<[^>]+>", string.Empty);

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
