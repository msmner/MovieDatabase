namespace MovieDatabase.Web.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Text.RegularExpressions;

    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class EditCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Content { get; set; }

        public string UserUserName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SanitizedContent => Regex.Replace(new HtmlSanitizer().Sanitize(this.Content), @"<[^>]+>", string.Empty);
    }
}
