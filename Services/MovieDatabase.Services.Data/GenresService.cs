namespace MovieDatabase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public IEnumerable<SelectListItem> GetGenres()
        {
            return this.genresRepository.All().Select(x => new SelectListItem(x.Type, x.Id)).ToList();
        }
    }
}
