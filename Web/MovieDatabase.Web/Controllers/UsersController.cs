namespace MovieDatabase.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels;
    using MovieDatabase.Web.ViewModels.Users;

    public class UsersController : Controller
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

        public IActionResult MyProfile(int? id = null, int page = 1)
        {
            var userId = string.Empty;

            if (id.HasValue)
            {
                userId = this.usersService.GetUserByMovieId(id);
            }
            else
            {
                userId = this.userManager.GetUserId(this.User);
            }

            var viewModel = new MyProfileViewModel();
            var movies = this.usersService.GetMyMovies<MyProfileMoviesViewModel>(userId, ItemsPerPage, (page - 1) * ItemsPerPage);
            viewModel.MyMovies = movies;
            var count = this.moviesService.GetMoviesCountByUserId(userId);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
