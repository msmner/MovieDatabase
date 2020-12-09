namespace MovieDatabase.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Movies;

    public class MoviesController : BaseController
    {
        private readonly IMoviesService moviesService;
        private readonly IGenresService genresService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly IFilesService filesService;

        public MoviesController(IMoviesService moviesService, IGenresService genresService, UserManager<ApplicationUser> userManager, Cloudinary cloudinary, IFilesService filesService)
        {
            this.moviesService = moviesService;
            this.genresService = genresService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.filesService = filesService;
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
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateMovieInputViewModel();
                viewModel.Genres = this.genresService.GetGenres();
                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var genreIds = new List<int>();

            foreach (var genreId in input.GenreIds)
            {
                genreIds.Add(genreId);
            }

            var imageResult = await this.filesService.UploadAsync(this.cloudinary, input.Image);
            var movieId = await this.moviesService.AddMovieAsync(input.Title, imageResult.Uri.ToString(), userId, genreIds);
            return this.RedirectToAction("Create", "Reviews", new { id = movieId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isUserAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);
            var userId = this.userManager.GetUserId(this.User);
            var isMovieCreatorLoggedIn = await this.moviesService.IsMovieCreatorLoggedIn(userId, id);

            if (isUserAdmin == true || isMovieCreatorLoggedIn == true)
            {
                await this.moviesService.Delete(id);
            }

            return this.Redirect("/");
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.moviesService.GetById<MovieDetailsViewModel>(id);
            return this.View(viewModel);
        }

        public IActionResult ByGenre(string genre)
        {
            var viewModel = new MoviesViewModel();
            var movies = this.moviesService.GetMoviesByGenre<MovieDetailsViewModel>(genre);
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }

        public IActionResult SearchByTitle(string searchString)
        {
            var viewModel = new MoviesViewModel();
            var movies = this.moviesService.GetMoviesByTitle<MovieDetailsViewModel>(searchString);
            viewModel.MyMovies = movies;
            return this.View(viewModel);
        }
    }
}
