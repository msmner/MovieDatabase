using MovieDatabase.Data.Common.Models;

namespace MovieDatabase.Data.Models
{
    public class MovieQuote : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
