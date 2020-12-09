namespace ForumSystem.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Required]
        public int ReviewId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}
