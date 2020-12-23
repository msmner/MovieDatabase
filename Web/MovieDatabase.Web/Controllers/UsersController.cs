﻿namespace MovieDatabase.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Comments;
    using MovieDatabase.Web.ViewModels.Movies;
    using MovieDatabase.Web.ViewModels.Reviews;

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
            string userId;

            if (id.HasValue)
            {
                userId = await this.usersService.GetUserByMovieIdAsync(id);
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

            var movies = await this.usersService.GetMoviesAsync<MovieDetailsViewModel>(userId, page, GlobalConstants.ItemsPerPage);
            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> UserReviews()
        {
            var viewModel = new AllReviewsViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var reviews = await this.usersService.GetReviewsAsync<SingleReviewViewModel>(userId);
            viewModel.Reviews = reviews;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> UserComments()
        {
            var viewModel = new AllCommentsViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var comments = await this.usersService.GetCommentsAsync<SingleCommentViewModel>(userId);
            viewModel.Comments = comments;
            return this.View(viewModel);
        }
    }
}
