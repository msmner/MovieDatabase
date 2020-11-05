﻿namespace MovieDatabase.Data.Models
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        // Add ICollection of movie quotes

        // Add property Title
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public string FirstQuote { get; set; }

        public string SecondQuote { get; set; }

        public string ThirdQuote { get; set; }
    }
}
