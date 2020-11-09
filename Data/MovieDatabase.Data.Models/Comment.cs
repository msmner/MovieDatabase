namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }

        public int? ParentId { get; set; }

        public Comment ParentComment { get; set; }
    }
}
