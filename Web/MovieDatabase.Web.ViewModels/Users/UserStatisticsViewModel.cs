namespace MovieDatabase.Web.ViewModels.Users
{
    using System.Collections.Generic;

    using MovieDatabase.Web.ViewModels.Reviews;

    public class UserStatisticsViewModel
    {
        public int MoviesCount { get; set; }

        public int CommentsCount { get; set; }

        public int VotesCount { get; set; }

        public IEnumerable<ReviewStatisticsViewModel> MostCommentedReviews { get; set; }

        public IEnumerable<ReviewStatisticsViewModel> MostVotedReviews { get; set; }
    }
}
