namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Models;

    public interface IGenresService
    {
        IEnumerable<SelectListItem> GetGenres();
    }
}
