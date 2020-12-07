namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IGenresService
    {
        IEnumerable<SelectListItem> GetGenres();
    }
}
