namespace MovieDatabase.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Comments;
    using MovieDatabase.Web.ViewModels.Movies;
    using MovieDatabase.Web.ViewModels.Reviews;
    using MovieDatabase.Web.ViewModels.Users;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IMoviesService moviesService;

        public UsersController(IUsersService usersService, IMoviesService moviesService)
        {
            this.usersService = usersService;
            this.moviesService = moviesService;
        }

        [Authorize]
        public async Task<IActionResult> UserMovies(int? id, int page = 1)
        {
            ApplicationUser user;
            string userId;

            if (id.HasValue)
            {
                user = await this.usersService.GetUserByMovieIdAsync(id);
                if (user == null)
                {
                    return this.NotFound();
                }

                userId = user.Id;
            }
            else
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            var viewModel = new MoviesViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = page,
                MoviesCount = this.moviesService.GetMoviesCountByUserId(userId),
            };

            var movies = await this.usersService.GetMoviesByUserAsync<MovieDetailsViewModel>(userId, page, GlobalConstants.ItemsPerPage);
            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> UserReviews()
        {
            var viewModel = new AllReviewsViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var reviews = await this.usersService.GetReviewsByUserAsync<SingleReviewViewModel>(userId);
            viewModel.Reviews = reviews;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> UserComments()
        {
            var viewModel = new AllCommentsViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var comments = await this.usersService.GetCommentsByUserAsync<SingleCommentViewModel>(userId);
            viewModel.Comments = comments;
            return this.View(viewModel);
        }

        public async Task<IActionResult> UserStatistics()
        {
            var viewModel = new UserStatisticsViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var moviesCount = this.moviesService.GetMoviesCountByUserId(userId);
            var commentsCountByUserId = this.usersService.CommentsCountByUserId(userId);
            var votesCountByUserId = this.usersService.VotesCountByUserId(userId);
            var mostCommentedReviews = await this.usersService.GetMostCommentedReviewsByUserId<ReviewStatisticsViewModel>(userId);
            var mostVotedReviews = await this.usersService.GetMostVotedReviewsByUserId<ReviewStatisticsViewModel>(userId);

            viewModel.MoviesCount = moviesCount;
            viewModel.CommentsCount = commentsCountByUserId;
            viewModel.VotesCount = votesCountByUserId;
            viewModel.MostCommentedReviews = mostCommentedReviews;
            viewModel.MostVotedReviews = mostVotedReviews;

            return this.View(viewModel);
        }
    }
}
