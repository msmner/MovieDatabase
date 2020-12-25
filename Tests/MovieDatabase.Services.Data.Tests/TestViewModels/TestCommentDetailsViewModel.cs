namespace MovieDatabase.Services.Data.Tests.TestViewModels
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class TestCommentDetailsViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
