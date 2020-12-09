namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Ganss.XSS;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class ReviewCommentViewModel : IMapFrom<Comment>
    {
        [Required]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserUserName { get; set; }

        [Required]
        [MaxLength(150)]
        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
    }
}
