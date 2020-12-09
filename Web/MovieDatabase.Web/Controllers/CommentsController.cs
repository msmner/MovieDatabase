namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string content, int? parentId, int reviewId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            parentId = parentId == 0 ? (int?)null : parentId;
            var userId = this.userManager.GetUserId(this.User);
            var movieId = await this.commentsService.CreateAsync(content, userId, reviewId, parentId);

            return this.RedirectToAction("Details", "Reviews", new { id = movieId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewId = this.commentsService.FindReviewByCommentId(id);
            await this.commentsService.Delete(id);
            return this.RedirectToAction("Details", "Reviews", new { id = reviewId });
        }
    }
}
