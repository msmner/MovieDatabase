namespace MovieDatabase.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Genres;
    using MovieDatabase.Web.ViewModels.Movies;

    public class MoviesController : BaseController
    {
        private readonly IMoviesService moviesService;
        private readonly IGenresService genresService;
        private readonly UserManager<ApplicationUser> userManager;

        public MoviesController(IMoviesService moviesService, IGenresService genresService, UserManager<ApplicationUser> userManager)
        {
            this.moviesService = moviesService;
            this.genresService = genresService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateMovieInputViewModel();
            viewModel.Genres = this.genresService.GetGenres();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieInputViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var genreIds = new List<string>();

            foreach (var genreId in input.GenreIds)
            {
                genreIds.Add(genreId);
            }

            var movieId = await this.moviesService.AddMovieAsync(input.Title, input.ImageUrl, userId, genreIds);
            return this.RedirectToAction("Create", "Reviews", new { id = movieId });
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
