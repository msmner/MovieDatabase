namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IGenresService
    {
        Task<IEnumerable<SelectListItem>> GetGenresAsync();
    }
}
