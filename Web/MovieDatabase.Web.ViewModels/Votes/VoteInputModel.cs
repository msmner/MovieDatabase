namespace ForumSystem.Web.ViewModels.Votes
{
    public class VoteInputModel
    {
        public int ReviewId { get; set; }

        public int CommentId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
