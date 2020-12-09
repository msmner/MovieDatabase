namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public Review()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(500)]
        public string Content { get; set; }

        [Required]
        public int Rating { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
