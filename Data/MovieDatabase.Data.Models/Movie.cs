namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Genres = new HashSet<Genre>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public string MovieQuotes { get; set; }
    }
}
