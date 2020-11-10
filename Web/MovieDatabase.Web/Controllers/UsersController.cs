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

        public IActionResult MyProfile()
        {
            var viewModel = new MyProfileViewModel();
            var userId = this.userManager.GetUserId(this.User);
            var movies = this.usersService.GetMyMovies<MyProfileMoviesViewModel>(userId);
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }

        public IActionResult UserProfile(int id)
        {
            var viewModel = new MyProfileViewModel();
            var userId = this.usersService.GetUserByMovieId(id);
            var movies = this.usersService.GetMyMovies<MyProfileMoviesViewModel>(userId);
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }
    }
}
