namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;

    public class Genre : BaseModel<int>
    {
        public Genre()
        {
            this.MovieGenres = new HashSet<MovieGenre>();
        }

        [Required]
        public string Type { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
