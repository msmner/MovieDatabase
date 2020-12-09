namespace ForumSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;
    using MovieDatabase.Data.Models;

    public class Vote : BaseModel<int>
    {
        [Required]
        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType Type { get; set; }
    }
}
