namespace MovieDatabase.Web.ViewModels.Genres
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class GenreViewModel : IMapFrom<Genre>
    {
        public int GenreId { get; set; }

        public string Type { get; set; }
    }
}
