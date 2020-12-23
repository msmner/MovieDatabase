namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Chats;
    using MovieDatabase.Web.ViewModels.Movies;

    public class ChatsController : BaseController
    {
        private readonly IMoviesService moviesService;

        public ChatsController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [Authorize]
        public async Task<IActionResult> Chat(int id)
        {
            var viewModel = new ChatViewModel();
            var movie = await this.moviesService.GetByIdAsync<MovieDetailsViewModel>(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            viewModel.Title = movie.Title;
            return this.View(viewModel);
        }
    }
}
