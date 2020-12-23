namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;

    public class GenresService : IGenresService
    {
        private readonly IRepository<Genre> genresRepository;

        public GenresService(IRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        {
            return await this.genresRepository.All().Select(x => new SelectListItem(x.Type, x.Id.ToString())).ToListAsync();
        }
    }
}
