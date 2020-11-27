﻿namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;

    using ForumSystem.Data.Models;
    using MovieDatabase.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public Review()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        public string MovieTitle { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
