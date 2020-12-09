namespace MovieDatabase.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels;
    using MovieDatabase.Web.ViewModels.Movies;

    public class HomeController : BaseController
    {
        private readonly IMoviesService moviesService;

        public HomeController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var viewModel = new MoviesViewModel();
            var movies = this.moviesService.GetTop10MoviesWithHighestRating<MovieDetailsViewModel>();

            if (movies == null)
            {
                return this.View();
            }

            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult ContactUs()
        {
            return this.View();
        }
    }
}
