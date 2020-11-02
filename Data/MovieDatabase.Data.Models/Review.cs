namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Content { get; set; }
    }
}
