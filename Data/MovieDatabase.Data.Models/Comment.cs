namespace MovieDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(150)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ReviewId { get; set; }

        public Review Review { get; set; }

        public int? ParentId { get; set; }

        public Comment ParentComment { get; set; }
    }
}
