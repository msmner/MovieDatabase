namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class CreateReviewInputViewModel : IMapTo<Review>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(300)]
        public string Content { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
