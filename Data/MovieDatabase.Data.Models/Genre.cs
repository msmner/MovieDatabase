namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class Genre : BaseDeletableModel<string>
    {
        public string Type { get; set; }
    }
}
