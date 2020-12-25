namespace MovieDatabase.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
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
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateMovieInputViewModel();
            viewModel.Genres = await this.genresService.GetGenresAsync();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateMovieInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateMovieInputViewModel();
                viewModel.Genres = await this.genresService.GetGenresAsync();
                return this.View(viewModel);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var imageResult = await this.filesService.UploadAsync(this.cloudinary, input.Image);
            var movieId = await this.moviesService.CreateMovieAsync(input.Title, imageResult.Uri.ToString(), userId, input.GenreIds, input.Quote, input.Description);
            return this.RedirectToAction("Create", "Reviews", new { id = movieId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isUserAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);
            var movie = await this.moviesService.GetByIdAsync<Movie>(id);

            if (isUserAdmin == true || movie.UserId == userId)
            {
                await this.moviesService.Delete(id);
            }
            else
            {
                return this.Unauthorized();
            }

            return this.RedirectToAction("UserMovies", "Users", new { id = userId });
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.moviesService.GetByIdAsync<MovieDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ByGenre(string genre, int page = 1)
        {
            var movies = await this.moviesService.GetMoviesByGenreAsync<MovieDetailsViewModel>(genre);
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
        public async Task<IActionResult> SearchByTitle(string searchString)
        {
            var viewModel = new MoviesViewModel();
            var movies = await this.moviesService.GetMoviesByTitleAsync<MovieDetailsViewModel>(searchString);
            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.moviesService.GetByIdAsync<EditMovieViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.NewGenres = await this.genresService.GetGenresAsync();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditMovieViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.NewGenres = await this.genresService.GetGenresAsync();
                return this.View(input);
            }

            var imageResult = await this.filesService.UploadAsync(this.cloudinary, input.Image);
            input.NewImageUrl = imageResult.Uri.ToString();
            await this.moviesService.UpdateAsync(input);
            return this.RedirectToAction(nameof(this.Details), new { input.Id });
        }
    }
}
