namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels;
    using MovieDatabase.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult MyProfile()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = new MyProfileViewModel();
            var movies = this.usersService.GetMyMovies<MyProfileMoviesViewModel>(userId);
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }

        public async Task<IActionResult> UsersProfile(int id)
        {
            var userId = await this.usersService.GetUserByMovieId(id);
            var movies = this.usersService.GetMyMovies<MyProfileMoviesViewModel>(userId);
            var viewModel = new MyProfileViewModel();
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }
    }
}
