namespace MovieDatabase.Web.ViewModels.Reviews
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class CreateReviewInputViewModel : IMapTo<Review>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public string FirstQuote { get; set; }

        public string SecondQuote { get; set; }

        public string ThirdQuote { get; set; }
    }
}
