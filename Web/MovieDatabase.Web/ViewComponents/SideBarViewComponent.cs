namespace MovieDatabase.Web.ViewComponents
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels;

    public class SidebarViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public SidebarViewComponent(
            IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new SideBarViewModel
            {
                RecentComments = this.commentsRepository.All().OrderByDescending(x => x.CreatedOn).To<RecentCommentViewModel>().Take(10).ToList(),
            };

            return this.View(model);
        }
    }
}
