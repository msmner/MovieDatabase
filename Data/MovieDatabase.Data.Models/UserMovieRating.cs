namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class UserMovieRating : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Rating { get; set; }
    }
}
