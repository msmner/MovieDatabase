namespace MovieDatabase.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels;
    using MovieDatabase.Web.ViewModels.Movies;
    using MovieDatabase.Web.ViewModels.Users;

    public class HomeController : BaseController
    {
        private readonly IMoviesService moviesService;
        private readonly IMessagesService messagesService;

        public HomeController(IMoviesService moviesService, IMessagesService messagesService)
        {
            this.moviesService = moviesService;
            this.messagesService = messagesService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new MoviesViewModel();
            var movies = await this.moviesService.GetMoviesWithMostComments<MovieDetailsViewModel>();

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

        [HttpPost]
        public async Task<IActionResult> ContactUs(UserContactFormInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.messagesService.SendContactMessage(input.Message, input.UserEmail);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
