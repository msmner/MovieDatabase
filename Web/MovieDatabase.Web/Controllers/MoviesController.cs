namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Movies;

    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;
        private readonly UserManager<ApplicationUser> userManager;

        public MoviesController(IMoviesService moviesService, UserManager<ApplicationUser> userManager)
        {
            this.moviesService = moviesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieInputViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var movieId = await this.moviesService.AddMovieAsync(input.Description,input.Title,input.ImageUrl, userId);
            return this.RedirectToAction("Create","Reviews",new { id = movieId});
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.moviesService.Delete(userId, id);

            return this.Redirect("/");
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.moviesService.GetById<MovieDetailsViewModel>(id);
            return this.View(viewModel);
        }
    }
}
