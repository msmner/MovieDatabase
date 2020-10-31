namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public string GenreType { get; set; }
    }
}
