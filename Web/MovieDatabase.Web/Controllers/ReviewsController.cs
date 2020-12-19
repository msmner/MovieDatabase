namespace MovieDatabase.Web.Controllers
{
    using System.Security.Claims;
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
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.reviewsService.AddReviewAsync(id, input.Content, input.Rating, userId);

            return this.RedirectToAction(nameof(this.Details), "Reviews", new { id });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var viewModel = this.reviewsService.GetReviewByMovieId<ReviewDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
