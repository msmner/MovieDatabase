namespace ForumSystem.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;

        public VotesController(
            IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Vote(VoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (input.ReviewId == 0 && input.CommentId == 0)
            {
                return this.NotFound();
            }

            await this.votesService.VoteAsync(input.ReviewId, input.CommentId, userId, input.IsUpVote);
            var votes = this.votesService.GetVotesCount(input.ReviewId, input.CommentId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
