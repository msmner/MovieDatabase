namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteAsync(int reviewId, int commentId, string userId, bool isUpVote);

        int GetVotesCount(int reviewId, int commentId);
    }
}
