namespace MovieDatabase.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(string content, int? parentId, int reviewId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            parentId = parentId == 0 ? (int?)null : parentId;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var movieId = await this.commentsService.CreateAsync(content, userId, reviewId, parentId);

            return this.RedirectToAction("Details", "Reviews", new { id = movieId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await this.commentsService.GetReviewByCommentIdAsync(id);
            if (review == null)
            {
                return this.NotFound();
            }

            await this.commentsService.DeleteAsync(id);
            return this.RedirectToAction("Details", "Reviews", new { id = review.Id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.commentsService.GetCommentByIdAsync<EditCommentViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.commentsService.UpdateAsync(id, input);
            var review = await this.commentsService.GetReviewByCommentIdAsync(id);
            if (review == null)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Details", "Reviews", new { Id = review.Id });
        }
    }
}
