namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class MovieGenre : BaseModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
