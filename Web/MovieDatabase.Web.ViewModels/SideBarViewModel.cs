namespace MovieDatabase.Web.ViewModels
{
    using System.Collections.Generic;

    public class SideBarViewModel
    {
        public IEnumerable<RecentCommentViewModel> RecentComments { get; set; }
    }
}
