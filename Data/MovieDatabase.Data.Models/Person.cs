namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;
    using MovieDatabase.Data.Models.Enums;

    public class Person : BaseDeletableModel<int>
    {
        public Person()
        {
            this.PersonMovies = new HashSet<PersonMovie>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PersonType PersonType { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Biography { get; set; }

        public virtual ICollection<PersonMovie> PersonMovies { get; set; }
    }
}
