namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteAsync(int reviewId, string userId, bool isUpVote);

        int GetVotes(int movieId);
    }
}