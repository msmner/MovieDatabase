namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMoviesService moviesService;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager, IMoviesService moviesService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.moviesService = moviesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string content, int? parentId, int reviewId)
        {
            parentId = parentId == 0 ? (int?)null : parentId;
            var userId = this.userManager.GetUserId(this.User);
            var movieId = await this.commentsService.CreateAsync(content, userId, reviewId, parentId);

            return this.RedirectToAction("Details", "Reviews", new { id = movieId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reviewId = this.commentsService.FindReviewByCommentId(id);
            await this.commentsService.Delete(id);
            return this.RedirectToAction("Details", "Reviews", new { id = reviewId });
        }
    }
}
