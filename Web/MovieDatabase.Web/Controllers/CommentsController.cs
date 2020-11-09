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

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string content, int? parentId, int reviewId)
        {
            parentId = parentId == 0 ? (int?)null : parentId;
            var userId = this.userManager.GetUserId(this.User);
            var movieId = await this.commentsService.CreateAsync(content, userId, reviewId, parentId);

            return this.RedirectToAction("Details", "Reviews", new { id = movieId });
        }
    }
}
