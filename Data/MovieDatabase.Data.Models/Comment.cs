namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public Comment()
        {
            this.Votes = new HashSet<Vote>();
        }

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

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
