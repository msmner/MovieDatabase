namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public GenreType GenreType { get; set; }
    }
}
