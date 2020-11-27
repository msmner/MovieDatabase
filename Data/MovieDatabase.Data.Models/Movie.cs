namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Genres = new HashSet<Genre>();
            this.Votes = new HashSet<Vote>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? ReviewId { get; set; }

        public string Quote { get; set; }

        public virtual Review Review { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public int VotesCount => this.Votes.Sum(x => (int)x.Type);
    }
}
