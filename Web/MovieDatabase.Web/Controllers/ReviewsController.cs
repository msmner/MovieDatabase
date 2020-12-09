namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Reviews;

    public class ReviewsController : BaseController
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [Authorize]
        public IActionResult Create(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateReviewInputViewModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateReviewInputViewModel();
                return this.View(viewModel);
            }

            await this.reviewsService.AddReviewAsync(id, input.Content, input.Rating);

            return this.RedirectToAction("Details", "Reviews", new { id });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var viewModel = this.reviewsService.GetReviewByMovieId<ReviewDetailsViewModel>(id);
            return this.View(viewModel);
        }
    }
}
