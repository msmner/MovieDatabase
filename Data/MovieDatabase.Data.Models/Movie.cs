namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int? ReviewId { get; set; }

        public virtual Review Review { get; set; }
    }
}
