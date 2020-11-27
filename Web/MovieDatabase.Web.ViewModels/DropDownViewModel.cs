namespace MovieDatabase.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MovieDatabase.Data.Models;

    public class DropDownViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
