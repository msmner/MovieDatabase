namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.PersonMovies = new HashSet<PersonMovie>();
            this.Genres = new HashSet<Genre>();
            this.MovieQuotes = new HashSet<MovieQuote>();
            this.Reviews = new HashSet<Review>();
            this.People = new HashSet<Person>();
        }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public virtual ICollection<PersonMovie> PersonMovies { get; set; }

        public virtual Rating Rating { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<MovieQuote> MovieQuotes { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
