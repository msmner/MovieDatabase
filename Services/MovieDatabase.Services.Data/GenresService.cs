namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public IEnumerable<SelectListItem> GetGenres()
        {
            return this.genresRepository.All().Select(x => new SelectListItem(x.Type, x.Id.ToString())).ToList();
        }
    }
}
