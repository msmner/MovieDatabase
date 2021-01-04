namespace MovieDatabase.Services.Data.Tests.TestViewModels
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class TestReviewStatisticsViewModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
    }
}
