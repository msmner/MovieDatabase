namespace MovieDatabase.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Movies;

    public class MoviesController : BaseController
    {
        private readonly IMoviesService moviesService;
        private readonly IGenresService genresService;
        private readonly Cloudinary cloudinary;
        private readonly IFilesService filesService;

        public MoviesController(IMoviesService moviesService, IGenresService genresService, Cloudinary cloudinary, IFilesService filesService)
        {
            this.moviesService = moviesService;
            this.genresService = genresService;
            this.cloudinary = cloudinary;
            this.filesService = filesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateMovieInputViewModel();
            viewModel.Genres = this.genresService.GetGenres();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateMovieInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateMovieInputViewModel();
                viewModel.Genres = this.genresService.GetGenres();
                return this.View(viewModel);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var imageResult = await this.filesService.UploadAsync(this.cloudinary, input.Image);
            var movieId = await this.moviesService.AddMovieAsync(input.Title, imageResult.Uri.ToString(), userId, input.GenreIds, input.Quote, input.Description);
            return this.RedirectToAction("Create", "Reviews", new { id = movieId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isUserAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);
            var isMovieCreatorLoggedIn = await this.moviesService.IsMovieCreatorLoggedIn(userId, id);

            if (isUserAdmin == true || isMovieCreatorLoggedIn == true)
            {
                await this.moviesService.Delete(id);
            }

            return this.RedirectToAction("UserProfile", "Users", new { id = userId });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var viewModel = this.moviesService.GetById<MovieDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.BadRequest();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult ByGenre(string genre, int page = 1)
        {
            var movies = this.moviesService.GetMoviesByGenre<MovieDetailsViewModel>(genre);
            var viewModel = new MoviesViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = page,
                MoviesCount = this.moviesService.GetMoviesCountByGenre(genre),
            };
            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult SearchByTitle(string searchString)
        {
            var viewModel = new MoviesViewModel();
            var movies = this.moviesService.GetMoviesByTitle<MovieDetailsViewModel>(searchString);
            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var viewModel = this.moviesService.GetById<EditMovieViewModel>(id);
            viewModel.NewGenres = this.genresService.GetGenres();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditMovieViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.NewGenres = this.genresService.GetGenres();
                return this.View(input);
            }

            var imageResult = await this.filesService.UploadAsync(this.cloudinary, input.Image);
            input.NewImageUrl = imageResult.Uri.ToString();
            await this.moviesService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }
    }
}
