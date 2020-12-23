namespace ForumSystem.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Repositories;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotesCount(int reviewId, int commentId)
        {
            var votes = 0;
            if (commentId == 0)
            {
                votes = this.votesRepository.All()
                .Where(x => x.ReviewId == reviewId).Sum(x => (int)x.Type);
            }
            else
            {
                votes = this.votesRepository.All()
                .Where(x => x.CommentId == commentId).Sum(x => (int)x.Type);
            }

            return votes;
        }

        // System.InvalidOperationException: A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbContext. - made the DbContext Transient in Startup Services //This is the other error I got Enumeration yielded no result - Tried formatting the code but got these error messages and couldnt fix them
        public async Task VoteAsync(int reviewId, int commentId, string userId, bool isUpVote)
        {
            Vote vote;
            if (commentId == 0)
            {
                vote = this.votesRepository.All()
                .FirstOrDefault(x => x.ReviewId == reviewId && x.UserId == userId);
                if (vote != null)
                {
                    vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
                }
                else
                {
                    vote = new Vote
                    {
                        ReviewId = reviewId,
                        UserId = userId,
                        Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                    };

                    await this.votesRepository.AddAsync(vote);
                }
            }
            else if (reviewId == 0)
            {
                vote = this.votesRepository.All()
                .FirstOrDefault(x => x.CommentId == commentId && x.UserId == userId);
                if (vote != null)
                {
                    vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
                }
                else
                {
                    vote = new Vote
                    {
                        CommentId = commentId,
                        UserId = userId,
                        Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                    };

                    await this.votesRepository.AddAsync(vote);
                }
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
