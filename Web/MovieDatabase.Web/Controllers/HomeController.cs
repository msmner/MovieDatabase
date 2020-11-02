namespace MovieDatabase.Web.Controllers
{
    using System.Diagnostics;

    using MovieDatabase.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Services.Data;
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
            if (this.User.Identity.IsAuthenticated)
            {
                var viewModel = this.moviesService.GetAll<HomePageMovieViewModel>();
                return this.View(viewModel);
            }

            return this.View();
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
    }
}
