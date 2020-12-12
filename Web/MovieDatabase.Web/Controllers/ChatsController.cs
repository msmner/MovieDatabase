namespace MovieDatabase.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Chats;

    public class ChatsController : BaseController
    {
        private readonly IMoviesService moviesService;

        public ChatsController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [Authorize]
        public IActionResult Chat(int id)
        {
            var viewModel = new ChatViewModel();
            var movie = this.moviesService.GetMovieById(id);
            viewModel.Title = movie.Title;
            return this.View(viewModel);
        }
    }
}
