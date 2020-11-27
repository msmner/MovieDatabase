namespace MovieDatabase.Web.ViewComponents
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels;

    public class DropDownViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public DropDownViewComponent(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new DropDownViewModel
            {
                Genres = this.genresRepository.AllAsNoTracking().OrderBy(x => x.Type).ToList(),
            };
            return this.View(model);
        }
    }
}
