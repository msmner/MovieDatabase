namespace MovieDatabase.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Movies;

    public class UsersController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IUsersService usersService;
        private readonly IMoviesService moviesService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, IMoviesService moviesService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.moviesService = moviesService;
            this.userManager = userManager;
        }

        public IActionResult UserProfile(int? id, int page = 1)
        {
            string userId;
            if (id.HasValue)
            {
                userId = this.usersService.GetUserByMovieId(id);
            }
            else
            {
                userId = this.userManager.GetUserId(this.User);
            }

            var viewModel = new MoviesViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                MoviesCount = this.moviesService.GetMoviesCountByUserId(userId),
            };

            var movies = this.usersService.GetMyMovies<MovieDetailsViewModel>(userId, page, ItemsPerPage);
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }
    }
}
