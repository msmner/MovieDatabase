namespace ForumSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;
    using MovieDatabase.Data.Models;

    public class Vote : BaseModel<int>
    {
        public int? ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public int? CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
