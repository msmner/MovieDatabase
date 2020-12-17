namespace MovieDatabase.Services.Data.Tests.TestViewModels
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class TestReviewDetailsViewModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
