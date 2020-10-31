namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class PersonMovie : BaseDeletableModel<int>
    {
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
