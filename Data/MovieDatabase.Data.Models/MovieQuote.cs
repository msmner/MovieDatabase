namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class MovieQuote : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
