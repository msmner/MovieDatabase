namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        // POST /api/votes
        // Request body: {"reviewId":1,"isUpVote":true}
        // Response body: {"votesCount":16}
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Review(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.votesService.VoteAsync(input.ReviewId, userId, input.IsUpVote);
            var votes = this.votesService.GetVotes(input.ReviewId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
