namespace MovieDatabase.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Movies;

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
        public IActionResult UserMovies(int? id, int page = 1)
        {
            string userId;

            if (id.HasValue)
            {
                userId = this.usersService.GetUserByMovieId(id);
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

            var movies = this.usersService.GetMovies<MovieDetailsViewModel>(userId, page, GlobalConstants.ItemsPerPage);
            viewModel.Movies = movies;
            return this.View(viewModel);
        }
    }
}
