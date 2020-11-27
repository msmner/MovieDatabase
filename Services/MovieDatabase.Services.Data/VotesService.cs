namespace ForumSystem.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public VotesService(IRepository<Vote> votesRepository, IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.votesRepository = votesRepository;
            this.moviesRepository = moviesRepository;
        }

        public int GetVotes(int reviewId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.ReviewId == reviewId).Sum(x => (int)x.Type);
            return votes;
        }

        public async Task VoteAsync(int reviewId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
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
                this.moviesRepository.All().FirstOrDefault(x => x.ReviewId == reviewId).Votes.Add(vote);
                await this.moviesRepository.SaveChangesAsync();
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
