namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
